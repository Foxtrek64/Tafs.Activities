//
//  Function.cs
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

namespace Tafs.Activities.Functions
{
    /// <summary>
    /// Provides a set of helper functions that map a lambda to a <see cref="Func{TResult}"/>.
    /// </summary>
    public static class Function
    {
        /// <inheritdoc cref="Func{TResult}"/>
        public static Func<TResult> AsFunc<TResult>(object lambda) => ReturnOrThrow(lambda as Func<TResult>);

        /// <inheritdoc cref="Func{T, TResult}"/>
        public static Func<T, TResult> AsFunc<T, TResult>(object lambda) => ReturnOrThrow(lambda as Func<T, TResult>);

        /// <inheritdoc cref="Func{T1, T2, TResult}"/>
        public static Func<T1, T2, TResult> AsFunc<T1, T2, TResult>(object lambda) => ReturnOrThrow(lambda as Func<T1, T2, TResult>);

        /// <inheritdoc cref="Func{T1, T2, T3, TResult}"/>
        public static Func<T1, T2, T3, TResult> AsFunc<T1, T2, T3, TResult>(object lambda) => ReturnOrThrow(lambda as Func<T1, T2, T3, TResult>);

        /// <inheritdoc cref="Func{T1, T2, T3, T4, TResult}"/>
        public static Func<T1, T2, T3, T4, TResult> AsFunc<T1, T2, T3, T4, TResult>(object lambda) => ReturnOrThrow(lambda as Func<T1, T2, T3, T4, TResult>);

        /// <inheritdoc cref="Func{T1, T2, T3, T4, T5, TResult}"/>
        public static Func<T1, T2, T3, T4, T5, TResult> AsFunc<T1, T2, T3, T4, T5, TResult>(object lambda) => ReturnOrThrow(lambda as Func<T1, T2, T3, T4, T5, TResult>);

        /// <inheritdoc cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/>
        public static Func<T1, T2, T3, T4, T5, T6, TResult> AsFunc<T1, T2, T3, T4, T5, T6, TResult>(object lambda) => ReturnOrThrow(lambda as Func<T1, T2, T3, T4, T5, T6, TResult>);

        /// <inheritdoc cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/>
        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> AsFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(object lambda) => ReturnOrThrow(lambda as Func<T1, T2, T3, T4, T5, T6, T7, TResult>);

        /// <inheritdoc cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/>
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> AsFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(object lambda) => ReturnOrThrow(lambda as Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>);

        private static TValue ReturnOrThrow<TValue>(TValue? value)
            => value ?? throw new InvalidOperationException("Provided value is not a lambda");
    }
}
