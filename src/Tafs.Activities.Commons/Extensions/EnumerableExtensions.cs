//
//  EnumerableExtensions.cs
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
using System.Threading.Tasks;

namespace Tafs.Activities.Commons.Extensions
{
    /// <summary>
    /// Provides a set of extensions for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
#if NETSTANDARD2_1_OR_GREATER || NET6_0
        /// <summary>
        /// Converts an async enumerable sequence to an enumerable sequence.
        /// </summary>
        /// <typeparam name="T">The type to iterate.</typeparam>
        /// <param name="source">The async enumerable.</param>
        /// <returns>A synchronous enumerable.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IEnumerable<T> ToBlockingEnumerable<T>(this IAsyncEnumerable<T> source)
        {
            return source is null
                ? throw new ArgumentNullException(nameof(source))
                : Core(source);

            static IEnumerable<T> Core(IAsyncEnumerable<T> source)
            {
                var e = source.GetAsyncEnumerator(default);

                try
                {
                    while (true)
                    {
                        if (!Wait(e.MoveNextAsync()))
                        {
                            break;
                        }

                        yield return e.Current;
                    }
                }
                finally
                {
                    Wait(e.DisposeAsync());
                }
            }
        }

        // NB: ValueTask and ValueTask<T> do not have to support blocking on a call to GetResult when backed by
        //     an IValueTaskSource or IValueTaskSource<T> implementation. Convert to a Task or Task<T> to do so
        //     in case the task hasn't completed yet.

        private static void Wait(ValueTask task)
        {
            var awaiter = task.GetAwaiter();

            if (!awaiter.IsCompleted)
            {
                task.AsTask().GetAwaiter().GetResult();
                return;
            }

            awaiter.GetResult();
        }

        private static T Wait<T>(ValueTask<T> task)
        {
            var awaiter = task.GetAwaiter();

            if (!awaiter.IsCompleted)
            {
                return task.AsTask().GetAwaiter().GetResult();
            }

            return awaiter.GetResult();
        }
#endif
    }
}
