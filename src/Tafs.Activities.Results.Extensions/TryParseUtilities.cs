//
//  TryParseUtilities.cs
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
using System.Globalization;
using System.Numerics;
using Remora.Results;
using Tafs.Activities.Results.Extensions.Errors;

namespace Tafs.Activities.Results.Extensions
{
    /// <summary>
    /// Adds utilities for parsing types.
    /// </summary>
    public static class TryParseUtilities
    {
#if NET7_0_OR_GREATER
        /// <summary>
        /// Attempts to parse the provided value and returns a <see cref="Result{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type to parse.</typeparam>
        /// <param name="value">The value to parse.</param>
        /// <param name="provider">An optional format provider.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed value or an error explaining why the parse failed.</returns>
        public static Result<TEntity> TryParseResult<TEntity>(ReadOnlySpan<char> value, IFormatProvider? provider)
            where TEntity : ISpanParsable<TEntity>
            => TEntity.TryParse(value, provider, out TEntity? result)
                ? result
                : new ParseError<TEntity>(value.ToString());

        /// <inheritdoc cref="TryParseResult{T}(ReadOnlySpan{char}, IFormatProvider?)"/>
        public static Result<TEntity> TryParseResult<TEntity>(string value, IFormatProvider? format)
            where TEntity : IParsable<TEntity>
            => TEntity.TryParse(value, format, out TEntity? result)
                ? result
                : new ParseError<TEntity>(value);

        /// <summary>
        /// A generic parser for converting INumber.TryParse(string, out INumber?) into a result value.
        /// </summary>
        /// <typeparam name="TNumber">The type of number to parse.</typeparam>
        /// <param name="value">The string value to parse.</param>
        /// <param name="numberStyles">The <see cref="NumberStyles"/> to use.</param>
        /// <param name="provider">The format provider to use.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed number or a parse error.</returns>
        public static Result<TNumber> TryParseResult<TNumber>(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            where TNumber : INumber<TNumber>
            => TNumber.TryParse(value, numberStyles, provider, out TNumber? result)
                ? result
                : new ParseError<TNumber>(value.ToString());

#elif NETSTANDARD2_1_OR_GREATER || NET6_0
        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<sbyte> TryParseSignedByte(ReadOnlySpan<char> value)
            => sbyte.TryParse(value, out sbyte result)
                ? result
                : new ParseError<sbyte>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<sbyte> TryParseSignedByte(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => sbyte.TryParse(value, numberStyles, provider, out sbyte result)
                ? result
                : new ParseError<sbyte>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<byte> TryParseByte(ReadOnlySpan<char> value)
            => byte.TryParse(value, out byte result)
                ? result
                : new ParseError<byte>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<byte> TryParseByte(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => byte.TryParse(value, numberStyles, provider, out byte result)
                ? result
                : new ParseError<byte>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<short> TryParseShort(ReadOnlySpan<char> value)
            => short.TryParse(value, out short result)
                ? result
                : new ParseError<short>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<short> TryParseShort(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => short.TryParse(value, numberStyles, provider, out short result)
                ? result
                : new ParseError<short>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<ushort> TryParseUnsignedShort(ReadOnlySpan<char> value)
            => ushort.TryParse(value, out ushort result)
                ? result
                : new ParseError<ushort>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<ushort> TryParseUnsignedShort(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => ushort.TryParse(value, numberStyles, provider, out ushort result)
                ? result
                : new ParseError<ushort>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<int> TryParseInteger(ReadOnlySpan<char> value)
            => int.TryParse(value, out int result)
                ? result
                : new ParseError<int>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<int> TryParseInteger(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => int.TryParse(value, numberStyles, provider, out int result)
                ? result
                : new ParseError<int>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<uint> TryParseUnsignedInteger(ReadOnlySpan<char> value)
            => uint.TryParse(value, out uint result)
                ? result
                : new ParseError<uint>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<uint> TryParseUnsignedInteger(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => uint.TryParse(value, numberStyles, provider, out uint result)
                ? result
                : new ParseError<uint>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<long> TryParseLong(ReadOnlySpan<char> value)
            => long.TryParse(value, out long result)
                ? result
                : new ParseError<long>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<long> TryParseLong(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => long.TryParse(value, numberStyles, provider, out long result)
                ? result
                : new ParseError<long>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<ulong> TryParseUnsignedLong(ReadOnlySpan<char> value)
            => ulong.TryParse(value, out ulong result)
                ? result
                : new ParseError<ulong>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<ulong> TryParseUnsignedLong(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => ulong.TryParse(value, numberStyles, provider, out ulong result)
                ? result
                : new ParseError<uint>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<float> TryParseFloat(ReadOnlySpan<char> value)
            => float.TryParse(value, out float result)
                ? result
                : new ParseError<float>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<float> TryParseFloat(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => float.TryParse(value, numberStyles, provider, out float result)
                ? result
                : new ParseError<float>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<double> TryParseDouble(ReadOnlySpan<char> value)
            => double.TryParse(value, out double result)
                ? result
                : new ParseError<double>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<double> TryParseDouble(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => double.TryParse(value, numberStyles, provider, out double result)
                ? result
                : new ParseError<double>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<decimal> TryParseDecimal(ReadOnlySpan<char> value)
            => decimal.TryParse(value, out decimal result)
                ? result
                : new ParseError<decimal>(value.ToString());

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<decimal> TryParseDecimal(ReadOnlySpan<char> value, NumberStyles numberStyles, IFormatProvider? provider)
            => decimal.TryParse(value, numberStyles, provider, out decimal result)
                ? result
                : new ParseError<decimal>(value.ToString());
#endif
#if NET6_0

        /// <summary>
        /// Attempts to parse the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed value or a parse error.</returns>
        public static Result<DateOnly> TryParseDateOnly(ReadOnlySpan<char> value)
            => DateOnly.TryParse(value, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        /// <summary>
        /// Attempts to parse the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="provider">An optional format provider.</param>
        /// <param name="dateTimeStyles">The <see cref="DateTimeStyles"/> to use.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed value or a parse error.</returns>
        public static Result<DateOnly> TryParseDateOnly(ReadOnlySpan<char> value, IFormatProvider? provider, DateTimeStyles dateTimeStyles)
            => DateOnly.TryParse(value, provider, dateTimeStyles, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        /// <inheritdoc cref="TryParseDateOnly(ReadOnlySpan{char})"/>
        public static Result<DateOnly> TryParseDateOnly(string value)
            => DateOnly.TryParse(value, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        /// <inheritdoc cref="TryParseDateOnly(ReadOnlySpan{char}, IFormatProvider?, DateTimeStyles)"/>
        public static Result<DateOnly> TryParseDateOnly(string value, IFormatProvider? provider, DateTimeStyles dateTimeStyles)
            => DateOnly.TryParse(value, provider, dateTimeStyles, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        /// <inheritdoc cref="TryParseDateOnly(ReadOnlySpan{char})"/>
        public static Result<TimeOnly> TryParseTimeOnly(ReadOnlySpan<char> value)
            => TimeOnly.TryParse(value, out TimeOnly result)
                ? result
                : new ParseError<TimeOnly>(value.ToString());

        /// <inheritdoc cref="TryParseDateOnly(ReadOnlySpan{char}, IFormatProvider?, DateTimeStyles)"/>
        public static Result<TimeOnly> TryParseTimeOnly(ReadOnlySpan<char> value, IFormatProvider? provider, DateTimeStyles dateTimeStyles)
            => TimeOnly.TryParse(value, provider, dateTimeStyles, out TimeOnly result)
                ? result
                : new ParseError<TimeOnly>(value.ToString());

        /// <inheritdoc cref="TryParseDateOnly(ReadOnlySpan{char})"/>
        public static Result<TimeOnly> TryParseTimeOnly(string value)
            => TimeOnly.TryParse(value, out TimeOnly result)
                ? result
                : new ParseError<TimeOnly>(value.ToString());

        /// <inheritdoc cref="TryParseDateOnly(ReadOnlySpan{char}, IFormatProvider?, DateTimeStyles)"/>
        public static Result<TimeOnly> TryParseTimeOnly(string value, IFormatProvider? provider, DateTimeStyles dateTimeStyles)
            => TimeOnly.TryParse(value, provider, dateTimeStyles, out TimeOnly result)
                ? result
                : new ParseError<TimeOnly>(value.ToString());

        // TODO: DateOnlyParseExact
        // TODO: TimeOnlyParseExact
        // TODO: DateTimeParse
        // TODO: DateTimeParseExact
        // TODO: DateTimeOffsetParse
        // TODO: DateTimeOffsetParseExact
        // TODO: Parse Generic

        /*
        public static Result<DateOnly> TryParseDateOnlyExact(string value, string? format)
            => DateOnly.TryParseExact(value, format, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        public static Result<DateOnly> TryParseDateOnlyExact(string value, string?[]? formats)
            => DateOnly.TryParseExact(value, formats, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        public static Result<DateOnly> TryParseDateOnlyExact(ReadOnlySpan<char> value, string?[]? formats)
            => DateOnly.TryParseExact(value, formats, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        public static Result<DateOnly> TryParseDateOnlyExact(ReadOnlySpan<char> value, ReadOnlySpan<char> format)
            => DateOnly.TryParseExact(value, format, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        public static Result<DateOnly> TryParseDateOnly(string value, string? format, IFormatProvider? provider, DateTimeStyles style)
            => DateOnly.TryParseExact(value, format, provider, style, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        public static Result<DateOnly> TryParseDateOnly(string value, string?[]? formats, IFormatProvider? provider, DateTimeStyles style)
            => DateOnly.TryParseExact(value, formats, provider, style, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        public static Result<DateOnly> TryParseDateOnly(ReadOnlySpan<char> value, string?[]? formats, IFormatProvider? provider, DateTimeStyles style)
            => DateOnly.TryParseExact(value, formats, provider, style, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());

        public static Result<DateOnly> TryParseDateOnly(ReadOnlySpan<char> value, ReadOnlySpan<char> format, IFormatProvider? provider, DateTimeStyles style)
            => DateOnly.TryParseExact(value, format, provider, style, out DateOnly result)
                ? result
                : new ParseError<DateOnly>(value.ToString());
        */
#endif

        /// <summary>
        /// Attempts to parse the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed value or a parse error.</returns>
        public static Result<sbyte> TryParseSignedByte(string value)
            => sbyte.TryParse(value, out sbyte result)
                ? result
                : new ParseError<sbyte>(value);

        /// <summary>
        /// Attempts to parse the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="numberStyles">The <see cref="NumberStyles"/> to use.</param>
        /// <param name="provider">An optioanl format provider.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the parsed value or a parse error.</returns>
        public static Result<sbyte> TryParseSignedByte(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => sbyte.TryParse(value, numberStyles, provider, out sbyte result)
                ? result
                : new ParseError<sbyte>(value);

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<byte> TryParseByte(string value)
            => byte.TryParse(value, out byte result)
                ? result
                : new ParseError<byte>(value);

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<byte> TryParseByte(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => byte.TryParse(value, numberStyles, provider, out byte result)
                ? result
                : new ParseError<byte>(value);

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<short> TryParseShort(string value)
            => short.TryParse(value, out short result)
                ? result
                : new ParseError<short>(value);

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<short> TryParseShort(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => short.TryParse(value, numberStyles, provider, out short result)
                ? result
                : new ParseError<short>(value);

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<ushort> TryParseUnsignedShort(string value)
            => ushort.TryParse(value, out ushort result)
                ? result
                : new ParseError<ushort>(value);

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<ushort> TryParseUnsignedShort(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => ushort.TryParse(value, numberStyles, provider, out ushort result)
                ? result
                : new ParseError<ushort>(value);

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<int> TryParseInteger(string value)
            => int.TryParse(value, out int result)
                ? result
                : new ParseError<int>(value);

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<int> TryParseInteger(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => int.TryParse(value, numberStyles, provider, out int result)
                ? result
                : new ParseError<int>(value);

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<uint> TryParseUnsignedInteger(string value)
            => uint.TryParse(value, out uint result)
                ? result
                : new ParseError<uint>(value);

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<uint> TryParseUnsignedInteger(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => uint.TryParse(value, numberStyles, provider, out uint result)
                ? result
                : new ParseError<uint>(value);

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<long> TryParseLong(string value)
            => long.TryParse(value, out long result)
                ? result
                : new ParseError<long>(value);

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<long> TryParseLong(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => long.TryParse(value, numberStyles, provider, out long result)
                ? result
                : new ParseError<long>(value);

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<ulong> TryParseUnsignedLong(string value)
            => ulong.TryParse(value, out ulong result)
                ? result
                : new ParseError<ulong>(value);

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<ulong> TryParseUnsignedLong(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => ulong.TryParse(value, numberStyles, provider, out ulong result)
                ? result
                : new ParseError<ulong>(value);

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<float> TryParseFloat(string value)
            => float.TryParse(value, out float result)
                ? result
                : new ParseError<float>(value);

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<float> TryParseFloat(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => float.TryParse(value, numberStyles, provider, out float result)
                ? result
                : new ParseError<float>(value);

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<double> TryParseDouble(string value)
            => double.TryParse(value, out double result)
                ? result
                : new ParseError<double>(value);

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<double> TryParseDouble(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => double.TryParse(value, numberStyles, provider, out double result)
                ? result
                : new ParseError<double>(value);

        /// <inheritdoc cref="TryParseSignedByte(string)"/>
        public static Result<decimal> TryParseDecimal(string value)
            => decimal.TryParse(value, out decimal result)
                ? result
                : new ParseError<decimal>(value);

        /// <inheritdoc cref="TryParseSignedByte(string, NumberStyles, IFormatProvider?)"/>
        public static Result<decimal> TryParseDecimal(string value, NumberStyles numberStyles, IFormatProvider? provider)
            => decimal.TryParse(value, numberStyles, provider, out decimal result)
                ? result
                : new ParseError<decimal>(value);
    }
}
