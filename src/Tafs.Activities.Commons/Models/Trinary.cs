//
//  Trinary.cs
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
using System.Runtime.CompilerServices;

namespace Tafs.Activities.Commons.Models
{
    /// <summary>
    /// Represents a trinary boolean value.
    /// </summary>
    [Serializable]
    public readonly struct Trinary
        : IComparable,
          IConvertible,
          IComparable<Trinary>,
#if NET7_0_OR_GREATER
          IEquatable<Trinary>,
          ISpanParsable<Trinary>
#else
          IEquatable<Trinary>
#endif
    {
        /// <summary>
        /// The true value.
        /// </summary>
        internal const int TrueValue = 1;

        /// <summary>
        /// The false value.
        /// </summary>
        internal const int FalseValue = 0;

        /// <summary>
        /// The indeterminate value.
        /// </summary>
        internal const int IndeterminateValue = -1;

        /// <summary>
        /// Internal string represenetation of <see cref="TrueValue"/>.
        /// </summary>
        internal const string TrueLiteral = "True";

        /// <summary>
        /// Internal string representation of <see cref="FalseValue"/>.
        /// </summary>
        internal const string FalseLiteral = "False";

        /// <summary>
        /// Internal string representation of <see cref="IndeterminateValue"/>.
        /// </summary>
        internal const string IndeterminateLiteral = "Indeterminate";

        private readonly bool? _value;

        /// <summary>
        /// Gets a string representation of True.
        /// </summary>
        public static readonly string TrueString = TrueLiteral;

        /// <summary>
        /// Gets a string representation of False.
        /// </summary>
        public static readonly string FalseString = FalseLiteral;

        /// <summary>
        /// Gets a string representation of Indeterminate.
        /// </summary>
        public static readonly string IndeterminateString = IndeterminateLiteral;

        /// <summary>
        /// Gets a thruthy <see cref="Trinary"/>.
        /// </summary>
        public static readonly Trinary True = new(true);

        /// <summary>
        /// Gets a falsy <see cref="Trinary"/>.
        /// </summary>
        public static readonly Trinary False = new(false);

        /// <summary>
        /// Gets an indeterminate <see cref="Trinary"/>.
        /// </summary>
        public static readonly Trinary Indeterminate = default;

        private Trinary(bool? value)
        {
            _value = value;
        }

        /// <inheritdoc/>
        /// <returns>1 for True, 0 for False, -1 for Indeterminate.</returns>
        public override int GetHashCode()
        {
            return (_value is { } value)
                ? value ? TrueValue : FalseValue
                : IndeterminateValue;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return (_value is { } value)
                ? value ? TrueLiteral : FalseLiteral
                : IndeterminateLiteral;
        }

        /// <inheritdoc/>
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        /// <inheritdoc/>
        bool IConvertible.ToBoolean(IFormatProvider? provider)
        {
            return _value switch
            {
                true => true,
                false => false,
                null => throw new InvalidCastException("Cannot convert from null to bool")
            };
        }

        private readonly int AsInt32()
        {
            return _value switch
            {
                true => TrueValue,
                false => FalseValue,
                null => IndeterminateValue
            };
        }

        /// <inheritdoc/>
        byte IConvertible.ToByte(IFormatProvider? provider)
        {
            return Convert.ToByte(AsInt32());
        }

        /// <inheritdoc/>
        char IConvertible.ToChar(IFormatProvider? provider)
        {
            throw new InvalidCastException("Cannot convert from Trinary to Char");
        }

        /// <inheritdoc/>
        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        {
            throw new InvalidCastException("Cannot convert from Trinary to DateTime");
        }

        /// <inheritdoc/>
        decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            return Convert.ToDecimal(AsInt32());
        }

        /// <inheritdoc/>
        double IConvertible.ToDouble(IFormatProvider? provider)
        {
            return Convert.ToDouble(AsInt32());
        }

        /// <inheritdoc/>
        short IConvertible.ToInt16(IFormatProvider? provider)
        {
            return Convert.ToInt16(AsInt32());
        }

        /// <inheritdoc/>
        int IConvertible.ToInt32(IFormatProvider? provider)
        {
            return AsInt32();
        }

        /// <inheritdoc/>
        long IConvertible.ToInt64(IFormatProvider? provider)
        {
            return Convert.ToInt64(AsInt32());
        }

        /// <inheritdoc/>
        sbyte IConvertible.ToSByte(IFormatProvider? provider)
        {
            return Convert.ToSByte(AsInt32());
        }

        /// <inheritdoc/>
        float IConvertible.ToSingle(IFormatProvider? provider)
        {
            return Convert.ToSingle(AsInt32());
        }

        /// <inheritdoc/>
        string IConvertible.ToString(IFormatProvider? provider)
        {
            return ToString();
        }

        /// <inheritdoc/>
        object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            return Convert.ChangeType(this, conversionType, provider);
        }

        /// <inheritdoc/>
        ushort IConvertible.ToUInt16(IFormatProvider? provider)
        {
            return Convert.ToUInt16(AsInt32());
        }

        /// <inheritdoc/>
        uint IConvertible.ToUInt32(IFormatProvider? provider)
        {
            return Convert.ToUInt32(AsInt32());
        }

        /// <inheritdoc/>
        ulong IConvertible.ToUInt64(IFormatProvider? provider)
        {
            return Convert.ToUInt64(AsInt32());
        }

        /// <inheritdoc/>
        public int CompareTo(object? obj)
        {
            if (obj is Trinary trinary)
            {
                return CompareTo(trinary);
            }
            else if (obj is bool b)
            {
                return CompareTo(b);
            }
            else if (obj is null)
            {
                return CompareTo(default(Trinary));
            }
            else
            {
                throw new ArgumentException("Argument must be a Trinary.");
            }
        }

        /// <inheritdoc/>
        public int CompareTo(Trinary other)
        {
            if (Equals(other))
            {
                return 0;
            }
            else if (_value == false)
            {
                return -1;
            }

            return 1;
        }

        /// <inheritdoc cref="CompareTo(Trinary)"/>
        public int CompareTo(bool other)
        {
            if (_value == other)
            {
                return 0;
            }
            else if (_value == false)
            {
                return -1;
            }
            return 1;
        }

#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        /// <inheritdoc cref="ISpanParsable{TSelf}.Parse(ReadOnlySpan{char}, IFormatProvider?)"/>
        public static Trinary Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            if (s.IsEmpty || s.IsWhiteSpace())
            {
                throw new ArgumentNullException(nameof(s));
            }

            return TryParse(s, provider, out Trinary result)
                ? result
                : throw new ArgumentException("Invalid value.", nameof(s));
        }

        /// <inheritdoc cref="ISpanParsable{TSelf}.TryParse(ReadOnlySpan{char}, IFormatProvider?, out TSelf)"/>
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Trinary result)
        {
            // Trinary.{Try}Parse allows for optional whitespace/null values before and after
            // the case-insensitive "true"/"false"/"indeterminate", but we don't expect those
            // to be the common case. We check for "true"/"false"/"indeterminate" case-insensitive
            // in the fast, inlined call path, and then only if neither match do we fall back
            // to trimming and making a second post-trimming attempt at matching those same strings.

            if (IsTrueStringIgnoreCase(s))
            {
                result = True;
                return true;
            }

            if (IsFalseStringIgnoreCase(s))
            {
                result = False;
                return true;
            }

            if (IsIndeterminateStringIgnoreCase(s))
            {
                result = Indeterminate;
                return false;
            }

            return TryParseUncommon(s, out result);

            [MethodImpl(MethodImplOptions.NoInlining)]
            static bool TryParseUncommon(ReadOnlySpan<char> value, out Trinary result)
            {
                // With "true" being 4 characters, even if we trim something from <= 4 chars,
                // it can't possibly match "true", "false", or "indeterminate"
                int originalLength = value.Length;
                if (originalLength >= 5)
                {
                    value = TrimWhiteSpaceAndNull(value);

                    // Value was trimmed. Try matching again.
                    if (value.Length != originalLength)
                    {
                        if (IsTrueStringIgnoreCase(value))
                        {
                            result = True;
                            return true;
                        }

                        if (IsFalseStringIgnoreCase(value))
                        {
                            result = False;
                            return true;
                        }

                        if (IsIndeterminateStringIgnoreCase(value))
                        {
                            result = Indeterminate;
                            return true;
                        }

                        result = null;
                        return false;
                    }
                }

                result = null;
                return false;
            }
        }

        /// <summary>
        /// Determines if the value is equal to <see cref="TrueLiteral"/>.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns><see langword="true"/> if the values are equal; otherwise, <see langword="false"/>.</returns>
        internal static bool IsTrueStringIgnoreCase(ReadOnlySpan<char> value)
        {
            // JIT inlines and unrolls this. See https://github.com/dotnet/runtime/pull/77398
            return value.Equals(TrueLiteral, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines if the value is equal to <see cref="FalseLiteral"/>.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns><see langword="true"/> if the values are equal; otherwise, <see langword="false"/>.</returns>
        internal static bool IsFalseStringIgnoreCase(ReadOnlySpan<char> value)
        {
            return value.Equals(FalseLiteral, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines if the value is equal to <see cref="IndeterminateLiteral"/>.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns><see langword="true"/> if the values are equal; otherwise, <see langword="false"/>.</returns>
        internal static bool IsIndeterminateStringIgnoreCase(ReadOnlySpan<char> value)
        {
            return value.Equals(IndeterminateLiteral, StringComparison.OrdinalIgnoreCase);
        }

        private static ReadOnlySpan<char> TrimWhiteSpaceAndNull(ReadOnlySpan<char> value)
        {
            int start = 0;
            while (start <= value.Length)
            {
                if (!char.IsWhiteSpace(value[start]) && value[start] != '\0')
                {
                    break;
                }
                start++;
            }

            int end = value.Length - 1;
            while (end >= start)
            {
                if (!char.IsWhiteSpace(value[end]) && value[end] != '\0')
                {
                    break;
                }
                end--;
            }

            return value.Slice(start, end - start + 1);
        }

        /// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)"/>
        public static Trinary Parse(string s, IFormatProvider? provider)
        {
            return Parse(s.AsSpan(), provider);
        }

        /// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)"/>
        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Trinary result)
        {
            return TryParse(s.AsSpan(), provider, out result);
        }
#endif

        /// <summary>
        /// Implicitly converts a boolean value into a <see cref="Trinary"/>.
        /// </summary>
        /// <param name="b">The boolean to convert.</param>
        public static implicit operator Trinary(bool? b)
        {
            return new(b);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is Trinary other
                ? Equals(other)
                : obj is null && Equals(Indeterminate);
        }

        /// <inheritdoc/>
        public bool Equals(Trinary other)
        {
            return _value == other._value;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static bool operator ==(Trinary left, Trinary right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Trinary left, Trinary right)
        {
            return !(left == right);
        }

        public static bool operator <(Trinary left, Trinary right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Trinary left, Trinary right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Trinary left, Trinary right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Trinary left, Trinary right)
        {
            return left.CompareTo(right) >= 0;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
