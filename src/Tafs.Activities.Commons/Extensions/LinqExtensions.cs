//
//  LinqExtensions.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tafs.Activities.Commons.Extensions
{
    /// <summary>
    /// A set of Linq-like extensions.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Performs the specified <paramref name="action"/> on each item in the collection.
        /// </summary>
        /// <typeparam name="TValue">The type of item to act upon.</typeparam>
        /// <param name="values">The collection.</param>
        /// <param name="action">The operation to perform on each item.</param>
        public static void ForEach<TValue>(this IEnumerable<TValue> values, Action<TValue> action)
        {
            foreach (var item in values)
            {
                action(item);
            }
        }

        /// <summary>
        /// Performs the specified <paramref name="task"/> on each item in the collection.
        /// </summary>
        /// <typeparam name="TValue">The type of item to act upon.</typeparam>
        /// <param name="values">The collection.</param>
        /// <param name="task">The asynchronous operation to perform on each item.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> for this operation.</param>
        /// <returns>A task representing the lifetime of this function..</returns>
        public static Task ForEachAsync<TValue>(this IEnumerable<TValue> values, Func<TValue, Task> task, CancellationToken cancellationToken = default)
        {
            return Task.WhenAll(values.Select(value => Task.Run(() => task.Invoke(value), cancellationToken)));
        }

        /// <summary>
        /// Performs the specified <paramref name="task"/> on each item in the collection.
        /// </summary>
        /// <typeparam name="TValue">The type of item to act upon.</typeparam>
        /// <param name="values">The collection.</param>
        /// <param name="task">The asynchronous operation to perform on each item.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> for this operation.</param>
        /// <returns>A task representing the lifetime of this function..</returns>
        public static Task ForEachAsync<TValue>(this IEnumerable<TValue> values, Func<TValue, CancellationToken, Task> task, CancellationToken cancellationToken = default)
        {
            return Task.WhenAll(values.Select(item => task.Invoke(item, cancellationToken)));
        }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        /// <inheritdoc cref="ForEachAsync{TValue}(IEnumerable{TValue}, Func{TValue, CancellationToken, Task}, CancellationToken)"/>
        public static async Task ForEachAsync<TValue>(this IAsyncEnumerable<TValue> values, Func<TValue, CancellationToken, Task> task, CancellationToken cancellationToken = default)
        {
            await foreach (var item in values)
            {
                await task.Invoke(item, cancellationToken);
            }
        }
#endif
    }
}
