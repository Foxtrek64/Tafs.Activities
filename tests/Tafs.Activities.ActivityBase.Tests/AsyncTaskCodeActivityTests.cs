//
//  AsyncTaskCodeActivityTests.cs
//
//  Author:
//       Devin Duanne <dduanne@tafs.com>
//
//  Copyright (c) TAFS, LLC.
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Activities;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

#pragma warning disable SA1402 // File may only contain a single type

namespace Tafs.Activities.ActivityBase.Tests
{
    /// <summary>
    /// Defines tests for the <see cref="AsyncTaskCodeActivity"/> and <see cref="AsyncTaskCodeActivity{TResult}"/> types.
    /// </summary>
    public class AsyncTaskCodeActivityTests
    {
        /// <summary>
        /// Determines whether the result is null.
        /// </summary>
        [Fact]
        public void ShouldReturnVoidResult()
        {
            var genericActivity = new AsyncTaskActivity<object>(_ => Task.FromResult<object>(null!));

            object? vr1 = null;
            object? vr2 = WorkflowInvoker.Invoke<object>(genericActivity);

            Assert.Equal(vr1, vr2);
        }

        /// <summary>
        /// Proves that exceptions bubble up properly.
        /// </summary>
        // [Fact]
        public void ShouldThrow()
        {
            Activity activity = new AsyncTaskActivity(_ => Task.FromException(new InvalidOperationException("@")));
            new Action(() => WorkflowInvoker.Invoke(activity)).ShouldThrow<InvalidOperationException>().Message.ShouldBe("@");
        }

        /// <summary>
        /// Proves that tasks cancel properly.
        /// </summary>
        /// <returns>A Task representing the lifetime of this test.</returns>
        [Fact]
        public async Task ShouldCancel()
        {
            Activity activity = new AsyncTaskActivity(token => Task.Delay(Timeout.Infinite, token));
            var invoker = new WorkflowInvoker(activity);
            var taskCompletionSource = new TaskCompletionSource<object>();
            InvokeCompletedEventArgs? args = null;
            invoker.InvokeCompleted += (sender, localArgs) =>
            {
                args = localArgs;
                taskCompletionSource.SetResult(null!);
            };
            invoker.InvokeAsync(invoker);
            invoker.CancelAsync(invoker);
            _ = await taskCompletionSource.Task;
            _ = args!.Error.ShouldBeOfType<WorkflowApplicationAbortedException>();
        }

        /// <summary>
        /// Proves the activity returns a constant result.
        /// </summary>
        /// <param name="value">The expected return value.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldReturnConstantResult(int value)
        {
            var activity = new AsyncTaskActivity<int>(async _ =>
            {
                await Task.Yield();
                return value;
            });
            var result = WorkflowInvoker.Invoke(activity);

            Assert.Equal(value, result);
        }

        /// <summary>
        /// Ensures the correct string is written to the <see cref="MemoryStream"/>.
        /// </summary>
        [Fact]
        public void ShouldWriteCorrectString()
        {
            const string stringToWrite = "Hello, World!";

            using var memory = new MemoryStream();
            Activity activity = new AsyncTaskActivity(async _ =>
            {
                using var writer = new StreamWriter(memory);
                await writer.WriteAsync(stringToWrite);
                writer.Flush();
            });
            _ = WorkflowInvoker.Invoke(activity);

            byte[] buffer = memory.ToArray();

            Assert.Equal(stringToWrite, Encoding.UTF8.GetString(buffer, 0, buffer.Length));
        }
    }

    /// <summary>
    /// A test implementation of a <see cref="AsyncTaskCodeActivity"/>.
    /// </summary>
    public class AsyncTaskActivity : AsyncTaskCodeActivity
    {
        private readonly Func<CancellationToken, Task> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncTaskActivity"/> class.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public AsyncTaskActivity(Func<CancellationToken, Task> action) => _action = action;

        /// <inheritdoc />
        protected override Task ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken) => _action(cancellationToken);
    }

    /// <summary>
    /// A test implementation of a <see cref="AsyncTaskCodeActivity{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">The type to return.</typeparam>
    public class AsyncTaskActivity<TResult> : AsyncTaskCodeActivity<TResult>
    {
        private readonly Func<CancellationToken, Task<TResult>> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncTaskActivity{TResult}"/> class.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public AsyncTaskActivity(Func<CancellationToken, Task<TResult>> action) => _action = action;

        /// <inheritdoc />
        protected override Task<TResult> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken) => _action(cancellationToken);
    }
}
