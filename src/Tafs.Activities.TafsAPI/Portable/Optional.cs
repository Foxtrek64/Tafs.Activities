//
//  Optional.cs
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

#if NET461_OR_GREATER

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Remora.Rest.Core
{
    /// <summary>
    /// Represents an optional value. This is mainly used for JSON de/serialization where a value can be either
    /// present, null, or completely missing.
    /// </summary>
    /// <remarks>
    /// While a <see cref="Nullable{T}"/> allows for a value to be logically present but contain a null value,
    /// <see cref="Optional{TValue}"/> allows for a value to be logically missing. For example, an optional without a
    /// value would never be serialized, but a nullable with a null value would (albeit as <see langword="null"/>).
    /// </remarks>
    /// <typeparam name="TValue">The inner type.</typeparam>
    [PublicAPI]
    public readonly struct Optional<TValue> : IOptional
    {
        private readonly TValue _value;

        /// <summary>
        /// Gets the value contained in the optional.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the optional does not contain a value.</exception>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public TValue Value
        {
            get
            {
                if (HasValue)
                {
                    return _value;
                }

                throw new InvalidOperationException("The optional did not contain a valid value.");
            }
        }

        /// <inheritdoc/>
        public bool HasValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Optional{TValue}"/> struct.
        /// </summary>
        /// <param name="value">The contained value.</param>
        public Optional(TValue value)
        {
            _value = value;
            HasValue = true;
        }

        /// <inheritdoc/>
        [MemberNotNullWhen(true, nameof(_value))]
        [MemberNotNullWhen(true, nameof(Value))]
        public bool IsDefined() => HasValue && _value is not null;

        /// <summary>
        /// Determines whether the optional has a defined value; that is, whether it both has a value and that value is non-null.
        /// </summary>
        /// <param name="value">The defined value.</param>
        /// <returns><see langword="true"/> if the optional has a value and that value is non-null; otherwise, <see langword="false" />.</returns>
        [MemberNotNullWhen(true, nameof(Value))]
        public bool IsDefined([NotNullWhen(true)] out TValue? value)
        {
            value = default;
            if (!IsDefined())
            {
                return false;
            }

            value = _value;
            return true;
        }

        /// <summary>
        /// Returns the value of the current <see cref="Optional{TValue}"/> or <see langword="default"/> if the current <see cref="Optional{TValue}"/> has no value.
        /// </summary>
        /// <returns>Returns the value of <see langword="this"/> or <see langword="default"/> if none is present.</returns>
        public TValue? OrDefault()
        {
            return IsDefined(out var value)
                ? value
                : default;
        }

        /// <summary>
        /// Returns the value of the current <see cref="Optional{TValue}"/> or <paramref name="defaultValue"/> if
        /// the current <see cref="Optional{TValue}"/> has no value or its value is null.
        /// </summary>
        /// <param name="defaultValue">The value to return if the optional is empty.</param>
        /// <returns>The value of the <see cref="Optional{TValue}"/> or <paramref name="defaultValue"/> if none is present.</returns>
        [return: NotNullIfNotNull(nameof(defaultValue))]
        public TValue? OrDefault(TValue? defaultValue)
        {
            return IsDefined(out var value)
                ? value
                : defaultValue;
        }

        /// <summary>
        /// Returns the value of the current <see cref="Optional{TValue}"/> or throws the exception in
        /// <paramref name="func"/> if the current <see cref="Optional{TValue}"/> has no value.
        /// </summary>
        /// <param name="func">The function generating an <see cref="Exception"/>.</param>
        /// <returns>The value of the <see cref="Optional{TValue}"/>.</returns>
        /// <exception cref="Exception">If the <see cref="Optional{TValue}"/> has no value.</exception>
        // Compile-time optimization: taking an exception here would lead to an allocation on every call; taking a static
        // Delegate that produces an Exception only allocates in the failing case.
        public TValue OrThrow([RequireStaticDelegate] Func<Exception> func)
        {
            return TryGet(out var value)
                ? value
                : throw func();
        }

        /// <summary>
        /// Gets the underlying value of the <see cref="Optional{TValue}"/> if it has one.
        /// </summary>
        /// <param name="value">The value of the <see cref="Optional{TValue}"/> or <see langword="default"/> if it has none.</param>
        /// <returns><see langword="true"/> if the <see cref="Optional{TValue}"/> has a value even when its value is <see langword="null"/>.</returns>
        /// <seealso cref="IsDefined(out TValue?)"/>
        public bool TryGet([MaybeNullWhen(false)] out TValue value)
        {
            if (HasValue)
            {
                value = _value;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Applies a mapping function to the value of the <see cref="Optional{TValue}"/> if it has one; otherwise, returns
        /// a new <see cref="Optional{TValue}"/> of the resulting type with no value.
        /// </summary>
        /// <typeparam name="TResult">The value for the output of the mapping result.</typeparam>
        /// <param name="mappingFunc">The mapping function.</param>
        /// <returns>A new optional with the mapping result if the current optional has a value; otherwise, an optional with no value.</returns>
        public Optional<TResult> Map<TResult>(Func<TValue, TResult> mappingFunc)
        {
            return HasValue
                ? mappingFunc(_value)
                : default(Optional<TResult>);
        }

        /// <summary>
        /// Implicitly converts actual values into an optional.
        /// </summary>
        /// <param name="value">The value.</param>
        public static implicit operator Optional<TValue>(TValue value)
        {
            return new(value);
        }

        /// <summary>
        /// Compares two optionals for equality.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns><see langword="true"/> if the operands are equal; otherwise <see langword="false"/>.</returns>
        public static bool operator ==(Optional<TValue> left, Optional<TValue> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two optionals for inequality.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns><see langword="true"/> if the operands are not equal; otherwise <see langword="false"/>.</returns>
        public static bool operator !=(Optional<TValue> left, Optional<TValue> right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Compares this instance for equality with another instance.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns><see langword="true" /> if the instances are considered equal; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Optional<TValue> other)
        {
            return EqualityComparer<TValue>.Default.Equals(_value, other._value) && HasValue == other.HasValue;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Optional<TValue> other && Equals(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(_value, HasValue);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return HasValue
                ? _value?.ToString() ?? "null"
                : "Empty";
        }
    }
}
#endif
