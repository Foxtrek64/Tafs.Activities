//
//  AsyncTaskCodeActivity{TResult}.cs
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
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx.Interop;

namespace Tafs.Activities.ActivityBase
{
    /// <summary>
    /// A base type for creating an asyncronous <see cref="Activity"/> using the TPL.
    /// </summary>
    /// <typeparam name="TResult">The underlying type of of the resulting <see cref="Task{TResult}"/>.</typeparam>
#if NET6_0_OR_GREATER
    [Obsolete("Please use the build-in AsyncTaskCodeActivity from System.Activities.")]
#endif
    public abstract class AsyncTaskCodeActivity<TResult> : AsyncCodeActivity<TResult>
    {
        /// <inheritdoc/>
        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            var cts = new CancellationTokenSource();
            context.UserState = cts;
            var task = ExecuteAsync(context, cts.Token);
            var tcs = new TaskCompletionSource<TResult>(state);

            return ApmAsyncFactory.ToBegin(task, callback, state);
        }

        /// <inheritdoc/>
        protected override TResult EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            var task = (Task<TResult>)result;
            try
            {
                if (!task.IsCompleted)
                {
                    task.Wait();
                }

                return task.Result;
            }
            catch (TaskCanceledException)
            {
                if (context.IsCancellationRequested)
                {
                    context.MarkCanceled();
                }

                throw;
            }
            catch (AggregateException aex)
            {
                foreach (var ex in aex.Flatten().InnerExceptions)
                {
                    if (ex is not TaskCanceledException)
                    {
                        continue;
                    }

                    if (context.IsCancellationRequested)
                    {
                        context.MarkCanceled();
                        break;
                    }
                }

                throw;
            }
        }

        /// <inheritdoc/>
        protected override void Cancel(AsyncCodeActivityContext context)
        {
            var cts = (CancellationTokenSource)context.UserState;
            cts.Cancel();
        }

        /// <summary>
        /// Executes this activity asynchronously.
        /// </summary>
        /// <param name="context">The <see cref="AsyncCodeActivityContext"/>.</param>
        /// <param name="cancellationToken">The<see cref="CancellationToken"/>.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        protected abstract Task<TResult> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken);
    }
}
