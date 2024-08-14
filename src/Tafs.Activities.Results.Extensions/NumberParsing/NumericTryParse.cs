//
//  NumericTryParse.cs
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Remora.Results;
using Tafs.Activities.Results.Extensions.Errors;

namespace Tafs.Activities.Results.Extensions.NumberParsing
{
    /// <summary>
    /// Provides utilities for parsing integral numeric types as Results.
    /// </summary>
    public static class NumericTryParse
    {
#if NET7_0_OR_GREATER
        /// <summary>
        /// A generic parser for converting INumber.TryParse(string, out INumber?) into a result value.
        /// </summary>
        /// <typeparam name="TNumber">The type of number to parse.</typeparam>
        /// <param name="value">The string value to parse.</param>
        /// <param name="numberStyles">The number style to use. Defaults to <see cref="NumberStyles.Integer"/>.</param>
        /// <param name="provider">The format provider to use. Defaults to <see langword="null"/>.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed number or a parse error.</returns>
        public static Result<TNumber> TryParse<TNumber>
        (
            ReadOnlySpan<char> value,
            NumberStyles numberStyles = NumberStyles.Integer,
            IFormatProvider? provider = null
        )
            where TNumber : INumber<TNumber>
        {
            return TNumber.TryParse(value, numberStyles, provider, out TNumber? result)
                ? result
                : new ParseError<TNumber>(value.ToString());
        }
#endif
#if NETSTANDARD2_1_OR_GREATER || NET6_0

        private static Result<TNumber> SystemTryParse<TNumber>(ReadOnlySpan<char> input)
            where TNumber : unmanaged
            => TryParseImplementation<TNumber>.Function(input);

        private static Result<TNumber> SystemTryParse<TNumber>
        (
            ReadOnlySpan<char> input,
            NumberStyles numberStyles,
            IFormatProvider? formatProvider
        )
            where TNumber : unmanaged
            => TryParseImplementation<TNumber>.Function2(input, numberStyles, formatProvider);

        /// <summary>
        /// An internal implementation of the generic TryParse function.
        /// </summary>
        /// <typeparam name="TNumber">The type to parse.</typeparam>
        internal static class TryParseImplementation<TNumber>
            where TNumber : unmanaged
        {
            /// <summary>
            /// A deleage which accepts a <see cref="ReadOnlySpan{T}"/> and executes TryParse().
            /// </summary>
            /// <param name="input">The string input.</param>
            /// <param name="result">The parsed value.</param>
            /// <returns><see langword="true"/> if the parse was successful; otherwise, <see langword="false"/>.</returns>
            internal delegate bool TryParseDelegate(ReadOnlySpan<char> input, [NotNullWhen(true)] out TNumber? result);

            /// <summary>
            /// A delegate which accepts a <see cref="ReadOnlySpan{T}"/> and returns a <typeparamref name="TNumber"/>.
            /// </summary>
            /// <param name="input">The input as a span of chars.</param>
            /// <returns>A <see cref="Result{TEntity}"/> containing the parse result or an error.</returns>
            internal delegate Result<TNumber> TryParseResultDelegate(ReadOnlySpan<char> input);

            /// <summary>
            /// A delegate which accepts a <see cref="ReadOnlySpan{T}"/> and returns a <typeparamref name="TNumber"/>.
            /// </summary>
            /// <param name="input">The input as a span of chars.</param>
            /// <param name="numberStyles">The number style to use.</param>
            /// <param name="formatProvider">The format provider to use, if any.</param>
            /// <returns>True if the parse was successful; otherwise, false.</returns>
            internal delegate Result<TNumber> TryParseResultDelegate2(ReadOnlySpan<char> input, NumberStyles numberStyles, IFormatProvider? formatProvider);

            /// <summary>
            /// A function which converts a string to an instance of <typeparamref name="TNumber" />.
            /// </summary>
#pragma warning disable SA1401 // Fields should be private
            internal static TryParseResultDelegate Function = (ReadOnlySpan<char> input) =>
#pragma warning restore SA1401 // Fields should be private
            {
                static Result<TNumber> Fail(ReadOnlySpan<char> input)
                {
                    return new InvalidOperationError("Type does not contain a method matching the signature TryParse(string, out TNumber?)");
                }

                Type[] argTypes = [typeof(ReadOnlySpan<char>), typeof(TNumber).MakeByRefType()];
                MethodInfo? tryParse = typeof(TNumber).GetMethod("TryParse", argTypes);

                if (tryParse is null)
                {
                    Function = Fail;
                }
                else
                {
                    var inputParameter = Expression.Parameter(argTypes[0]);
                    var outputParameter = Expression.Parameter(argTypes[1]);
                    var call = Expression.Call(tryParse, inputParameter, outputParameter);
                    var expression = Expression.Lambda<TryParseDelegate>(call, inputParameter, outputParameter);
                    var func = expression.Compile();

                    Function = (it) => func(it, out TNumber? number)
                        ? number.Value
                        : new ParseError<TNumber>(it.ToString());
                }

                return Function(input);
            };

            /// <summary>
            /// A function which converts a string to an instance of <typeparamref name="TNumber" />.
            /// </summary>
#pragma warning disable SA1401 // Fields should be private
            internal static TryParseResultDelegate2 Function2 =
#pragma warning restore SA1401 // Fields should be private
            (
                ReadOnlySpan<char> input,
                NumberStyles numberStyles,
                IFormatProvider? formatProvider
            ) =>
            {
                static Result<TNumber> Fail(ReadOnlySpan<char> input, NumberStyles numberStyles, IFormatProvider? formatProvider)
                {
                    return new InvalidOperationError("Type does not contain a method matching the signature TryParse(string, out TNumber?)");
                }

                Type[] argTypes = [typeof(ReadOnlySpan<char>), typeof(NumberStyles), typeof(IFormatProvider), typeof(TNumber).MakeByRefType()];
                MethodInfo? tryParse = typeof(TNumber).GetMethod("TryParse", argTypes);
                Function2 = (TryParseResultDelegate2)(tryParse is null
                    ? Fail
                    : tryParse.CreateDelegate(typeof(TryParseResultDelegate2)));

                return Function2(input, numberStyles, formatProvider);
            };
        }

        /// <summary>
        /// A generic parser for converting INumber.TryParse(string, out INumber?) into a result value.
        /// </summary>
        /// <typeparam name="TNumber">The type of number to parse.</typeparam>
        /// <param name="input">The string value to parse.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed number or a parse error.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TNumber> TryParse<TNumber>(ReadOnlySpan<char> input)
            where TNumber : unmanaged
        {
            if (typeof(TNumber) == typeof(sbyte))
            {
                return sbyte.TryParse(input, out var result)
                    ? Unsafe.As<sbyte, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(byte))
            {
                return byte.TryParse(input, out var result)
                    ? Unsafe.As<byte, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(short))
            {
                return short.TryParse(input, out var result)
                    ? Unsafe.As<short, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(ushort))
            {
                return ushort.TryParse(input, out var result)
                    ? Unsafe.As<ushort, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(int))
            {
                return int.TryParse(input, out var result)
                    ? Unsafe.As<int, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(uint))
            {
                return uint.TryParse(input, out var result)
                    ? Unsafe.As<uint, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(long))
            {
                return long.TryParse(input, out var result)
                    ? Unsafe.As<long, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(ulong))
            {
                return ulong.TryParse(input, out var result)
                    ? Unsafe.As<ulong, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(float))
            {
                return float.TryParse(input, out var result)
                    ? Unsafe.As<float, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(double))
            {
                return double.TryParse(input, out var result)
                    ? Unsafe.As<double, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(decimal))
            {
                return decimal.TryParse(input, out var result)
                    ? Unsafe.As<decimal, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else
            {
                return SystemTryParse<TNumber>(input);
            }
        }

        /// <summary>
        /// A generic parser for converting INumber.TryParse(string, out INumber?) into a result value.
        /// </summary>
        /// <typeparam name="TNumber">The type of number to parse.</typeparam>
        /// <param name="input">The string value to parse.</param>
        /// <param name="numberStyles">The number style to use. Defaults to <see cref="NumberStyles.Integer"/>.</param>
        /// <param name="provider">The format provider to use. Defaults to <see langword="null"/>.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed number or a parse error.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TNumber> TryParse<TNumber>
        (
            ReadOnlySpan<char> input,
            NumberStyles numberStyles,
            IFormatProvider? provider = null
        )
                where TNumber : unmanaged
        {
            if (typeof(TNumber) == typeof(sbyte))
            {
                return sbyte.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<sbyte, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(byte))
            {
                return byte.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<byte, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(short))
            {
                return short.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<short, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(ushort))
            {
                return ushort.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<ushort, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(int))
            {
                return int.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<int, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(uint))
            {
                return uint.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<uint, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(long))
            {
                return long.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<long, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(ulong))
            {
                return ulong.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<ulong, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(float))
            {
                return float.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<float, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(double))
            {
                return double.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<double, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else if (typeof(TNumber) == typeof(decimal))
            {
                return decimal.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<decimal, TNumber>(ref result)
                    : new ParseError<TNumber>(input.ToString());
            }
            else
            {
                return SystemTryParse<TNumber>(input, numberStyles, provider);
            }
        }
#endif
#if NET461_OR_GREATER
        private static Result<TNumber> SystemTryParse<TNumber>(string input)
            where TNumber : unmanaged
            => TryParseImplementation<TNumber>.Function(input, out TNumber? result)
                ? result!.Value
                : new ParseError<TNumber>(input.ToString());

        private static Result<TNumber> SystemTryParse<TNumber>
        (
            string input,
            NumberStyles numberStyles,
            IFormatProvider? formatProvider
        )
            where TNumber : unmanaged
            => TryParseImplementation<TNumber>.Function2(input, numberStyles, formatProvider, out TNumber? result)
                ? result!.Value
                : new ParseError<TNumber>(input.ToString());

        /// <summary>
        /// An internal implementation of the generic TryParse function.
        /// </summary>
        /// <typeparam name="TNumber">The type to parse.</typeparam>
        internal static class TryParseImplementation<TNumber>
            where TNumber : unmanaged
        {
            /// <summary>
            /// A delegate which accepts a <see cref="string"/> and returns a <typeparamref name="TNumber"/>.
            /// </summary>
            /// <param name="input">The input as a span of chars.</param>
            /// <param name="result">The parse result.</param>
            /// <returns>True if the parse was successful; otherwise, false.</returns>
            internal delegate bool TryParseDelegate(string input, [NotNullWhen(true)] out TNumber? result);

            /// <summary>
            /// A delegate which accepts a <see cref="string"/> and returns a <typeparamref name="TNumber"/>.
            /// </summary>
            /// <param name="input">The input as a span of chars.</param>
            /// <param name="numberStyles">The number style to use.</param>
            /// <param name="formatProvider">The format provider to use, if any.</param>
            /// <param name="result">The parse result.</param>
            /// <returns>True if the parse was successful; otherwise, false.</returns>
            internal delegate bool TryParseDelegate2(string input, NumberStyles numberStyles, IFormatProvider? formatProvider, [NotNullWhen(true)] out TNumber? result);

            /// <summary>
            /// A function which converts a string to an instance of <typeparamref name="TNumber" />.
            /// </summary>
#pragma warning disable SA1401 // Fields should be private
            internal static TryParseDelegate Function = (string input, [NotNullWhen(true)] out TNumber? result) =>
#pragma warning restore SA1401 // Fields should be private
            {
                static bool Fail(string input, [NotNullWhen(true)] out TNumber? result)
                {
                    result = default;
                    return false;
                }

                Type[] argTypes = [typeof(string), typeof(TNumber).MakeByRefType()];
                MethodInfo? tryParse = typeof(TNumber).GetMethod("TryParse", argTypes);
                Function = (TryParseDelegate)(tryParse is null
                    ? Fail
                    : tryParse.CreateDelegate(typeof(TryParseDelegate)));
                return Function(input, out result);
            };

            /// <summary>
            /// A function which converts a string to an instance of <typeparamref name="TNumber" />.
            /// </summary>
#pragma warning disable SA1401 // Fields should be private
            internal static TryParseDelegate2 Function2 =
#pragma warning restore SA1401 // Fields should be private
            (
                string input,
                NumberStyles numberStyles,
                IFormatProvider? formatProvider,
                [NotNullWhen(true)] out TNumber? result
            ) =>
            {
                static bool Fail(string input, NumberStyles numberStyles, IFormatProvider? formatProvider, out TNumber? result)
                {
                    result = default;
                    return false;
                }

                Type[] argTypes = [typeof(ReadOnlySpan<char>), typeof(NumberStyles), typeof(IFormatProvider), typeof(TNumber).MakeByRefType()];
                MethodInfo? tryParse = typeof(TNumber).GetMethod("TryParse", argTypes);
                Function2 = (TryParseDelegate2)(tryParse is null
                    ? Fail
                    : tryParse.CreateDelegate(typeof(TryParseDelegate2)));

                return Function2(input, numberStyles, formatProvider, out result);
            };
        }

        /// <summary>
        /// A generic parser for converting INumber.TryParse(string, out INumber?) into a result value.
        /// </summary>
        /// <typeparam name="TNumber">The type of number to parse.</typeparam>
        /// <param name="input">The string value to parse.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed number or a parse error.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TNumber> TryParse<TNumber>(string input)
            where TNumber : unmanaged
        {
            if (typeof(TNumber) == typeof(sbyte))
            {
                return sbyte.TryParse(input, out var result)
                    ? Unsafe.As<sbyte, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(byte))
            {
                return byte.TryParse(input, out var result)
                    ? Unsafe.As<byte, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(short))
            {
                return short.TryParse(input, out var result)
                    ? Unsafe.As<short, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(ushort))
            {
                return ushort.TryParse(input, out var result)
                    ? Unsafe.As<ushort, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(int))
            {
                return int.TryParse(input, out var result)
                    ? Unsafe.As<int, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(uint))
            {
                return uint.TryParse(input, out var result)
                    ? Unsafe.As<uint, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(long))
            {
                return long.TryParse(input, out var result)
                    ? Unsafe.As<long, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(ulong))
            {
                return ulong.TryParse(input, out var result)
                    ? Unsafe.As<ulong, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(float))
            {
                return float.TryParse(input, out var result)
                    ? Unsafe.As<float, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(double))
            {
                return double.TryParse(input, out var result)
                    ? Unsafe.As<double, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(decimal))
            {
                return decimal.TryParse(input, out var result)
                    ? Unsafe.As<decimal, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else
            {
                Type[] argTypes = [typeof(string), typeof(TNumber).MakeByRefType()];
                MethodInfo? tryParse = typeof(TNumber).GetMethod("TryParse", argTypes);

                if (tryParse is null)
                {
                    return new InvalidOperationError("Type does not contain a method matching the signature TryParse(string, out TNumber?)");
                }

                object?[] args = [input, null];
                return (bool)tryParse.Invoke(null, args)
                    ? (TNumber)args[1]!
                    : new ParseError<TNumber>(input);
            }
        }

        /// <summary>
        /// A generic parser for converting INumber.TryParse(string, out INumber?) into a result value.
        /// </summary>
        /// <typeparam name="TNumber">The type of number to parse.</typeparam>
        /// <param name="input">The string value to parse.</param>
        /// <param name="numberStyles">The number style to use. Defaults to <see cref="NumberStyles.Integer"/>.</param>
        /// <param name="provider">The format provider to use. Defaults to <see langword="null"/>.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed number or a parse error.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Result<TNumber> TryParse<TNumber>
        (
            string input,
            NumberStyles numberStyles,
            IFormatProvider? provider = null
        )
            where TNumber : unmanaged
        {
            if (typeof(TNumber) == typeof(sbyte))
            {
                return sbyte.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<sbyte, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(byte))
            {
                return byte.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<byte, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(short))
            {
                return short.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<short, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(ushort))
            {
                return ushort.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<ushort, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(int))
            {
                return int.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<int, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(uint))
            {
                return uint.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<uint, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(long))
            {
                return long.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<long, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(ulong))
            {
                return ulong.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<ulong, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(float))
            {
                return float.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<float, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(double))
            {
                return double.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<double, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else if (typeof(TNumber) == typeof(decimal))
            {
                return decimal.TryParse(input, numberStyles, provider, out var result)
                    ? Unsafe.As<decimal, TNumber>(ref result)
                    : new ParseError<TNumber>(input);
            }
            else
            {
                Type[] argTypes = [typeof(string), typeof(NumberStyles), typeof(IFormatProvider), typeof(TNumber).MakeByRefType()];
                MethodInfo? tryParse = typeof(TNumber).GetMethod("TryParse", argTypes);

                if (tryParse is null)
                {
                    return new InvalidOperationError("Type does not contain a method matching the signature TryParse(string, out TNumber?)");
                }

                object?[] args = [input, numberStyles, provider, null];
                return (bool)tryParse.Invoke(null, args)
                    ? (TNumber)args[3]!
                    : new ParseError<TNumber>(input);
            }
        }
#endif
    }
}
