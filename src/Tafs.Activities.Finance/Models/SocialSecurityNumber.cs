//
//  SocialSecurityNumber.cs
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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using JetBrains.Annotations;
using Remora.Results;
using Tafs.Activities.Finance.Extensions;
using Tafs.Activities.Results.Extensions.Errors;

namespace Tafs.Activities.Finance.Models
{
    /// <summary>
    /// Represents a 9-digit United States social security number.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public readonly struct SocialSecurityNumber
#pragma warning disable SA1001 // Commas should not be preceeded by whitespace.
        : IFormattable
        , IEquatable<SocialSecurityNumber>
#if NET7_0_OR_GREATER
        , IEqualityOperators<SocialSecurityNumber, SocialSecurityNumber, bool>
#endif
#pragma warning restore SA1001
    {
        /// <summary>
        /// Gets the first three digits of the SSN. Formerly used to denote geographic location.
        /// </summary>
        public readonly short AreaNumber;

        /// <summary>
        /// Gets the digits in position 4 and 5 of the SSN.
        /// </summary>
        public readonly byte GroupNumber;

        /// <summary>
        /// Gets a number which is incremented for each issued SSN.
        /// </summary>
        public readonly short SerialNumber;

        /// <summary>
        /// Gets a default <see cref="SocialSecurityNumber"/> of 000-00-0000.
        /// </summary>
        /// <remarks>
        /// This social security number is invalid.
        /// </remarks>
        public static SocialSecurityNumber Default { get; } = default;

        /// <summary>
        /// Gets a <see cref="SocialSecurityNumber"/> used as a sample for advertisements and documentation by the Social Security Administration.
        /// </summary>
        /// <remarks>219-09-9999.</remarks>
        public static SocialSecurityNumber SampleNumber { get; } = new SocialSecurityNumber(219, 09, 9999);

        /// <summary>
        /// Gets a decommissioned <see cref="SocialSecurityNumber"/> once issued to an employee of Woolsworth Wallets.
        /// </summary>
        /// <remarks>078-05-1120.</remarks>
        /// <seealso href="https://www.ssa.gov/history/ssn/misused.html"/>
        public static SocialSecurityNumber WoolsworthSSN { get; } = new SocialSecurityNumber(078, 05, 1120);

        private SocialSecurityNumber(short areaNumber, byte groupNumber, short serialNumber)
        {
            AreaNumber = areaNumber;
            GroupNumber = groupNumber;
            SerialNumber = serialNumber;
        }

        /// <inheritdoc cref="FromString(string)"/>
        /// <returns>The parsed SocialSecurityNumber.</returns>
        /// <exception cref="ArgumentException">Thrown when a parse error occurs.</exception>
        public static SocialSecurityNumber Parse(string ssn)
        {
            Result<SocialSecurityNumber> parseResult = FromString(ssn);

            return parseResult.MapOrElse
            (
                it => it,
                (error, _) => throw new ArgumentException(error.Message, nameof(ssn))
            );
        }

        /// <summary>
        /// Attempts to parse the provided string into an instance of <see cref="SocialSecurityNumber"/>.
        /// </summary>
        /// <param name="ssn">The social security number to validate.</param>
        /// <param name="result">The parsed <see cref="SocialSecurityNumber"/>.</param>
        /// <returns><see langword="true" /> if the parse succeeded; otherwise, <see langword="false" />.</returns>
        public static bool TryParse(string ssn, [NotNullWhen(true)] out SocialSecurityNumber result)
        {
            return FromString(ssn).IsDefined(out result);
        }

        /// <summary>
        /// Attempts to parse the provided string into an instance of <see cref="SocialSecurityNumber"/>.
        /// </summary>
        /// <param name="ssn">The social security number to validate.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the <see cref="SocialSecurityNumber"/> or a failure result.</returns>
        public static Result<SocialSecurityNumber> FromString(string ssn)
        {
            if (string.IsNullOrWhiteSpace(ssn))
            {
                return new ArgumentNullError(nameof(ssn));
            }

            // Clean formatting
            ssn = ssn.Trim();
            ssn = ssn.Replace("-", string.Empty).Replace(" ", string.Empty);

            // Ensure length
            if (ssn.Length > 9)
            {
                return new ArgumentOutOfRangeError(nameof(ssn), "Provided value was longer than 9 characters.");
            }

            if (ssn.Length < 9)
            {
                return new ArgumentOutOfRangeError(nameof(ssn), "Provided value was shorter than 9 characters.");
            }

            Result allNumbers = ssn.ToCharArray().AllMatch
            (
                $"Social Security Numbers may only consist of the numbers [0-9], hyphens, and spaces.",
                "0123456789".Contains
            );
            if (!allNumbers.IsSuccess)
            {
                return Result<SocialSecurityNumber>.FromError(allNumbers);
            }

            short areaNumber = (short)(
                (short.Parse(ssn[0].ToString()) * 100) +
                (short.Parse(ssn[1].ToString()) * 10) +
                short.Parse(ssn[2].ToString())
            );

            byte groupNumber = (byte)(
                (byte.Parse(ssn[3].ToString()) * 10) +
                byte.Parse(ssn[4].ToString())
            );

            short serialNumber = (short)(
                (short.Parse(ssn[5].ToString()) * 1000) +
                (short.Parse(ssn[6].ToString()) * 100) +
                (short.Parse(ssn[7].ToString()) * 10) +
                short.Parse(ssn[8].ToString())
            );

            var number = new SocialSecurityNumber(areaNumber, groupNumber, serialNumber);

            var validationResult = Validate(number);

            return validationResult.IsSuccess
                ? number
                : Result<SocialSecurityNumber>.FromError(validationResult);
        }

        /// <summary>
        /// Validates a Social Security Number to ensure none of the following are met:
        /// <list type="bullet">
        ///     <item>Contains all zeroes in any specific group (000-##-####, ###-00-####, or ###-##-0000).</item>
        ///     <item>Begins with '666'.</item>
        ///     <item>Begins with any value from '900-999'.</item>
        ///     <item>Matches <see cref="WoolsworthSSN"/>.</item>
        ///     <item>Matches <see cref="SampleNumber"/>.</item>
        /// </list>
        /// </summary>
        /// <param name="ssn">The <see cref="SocialSecurityNumber"/> to validate.</param>
        /// <returns>A result indicating whether the validation succeeded.</returns>
        public static Result Validate(SocialSecurityNumber ssn)
        {
            // Default values
            if (ssn.AreaNumber == Default.AreaNumber)
            {
                return new ValidationError("The area number cannot be '000'.");
            }

            if (ssn.GroupNumber == Default.GroupNumber)
            {
                return new ValidationError("The group number cannot be '00'.");
            }

            if (ssn.SerialNumber == Default.SerialNumber)
            {
                return new ValidationError("The serial number cannot be '0000'");
            }

            // Starts with invalid numbers.
            if (ssn.AreaNumber == 666)
            {
                return new ValidationError("The area number cannot be '666'.");
            }

            if (ssn.AreaNumber is >= 900 and <= 999)
            {
                return new ValidationError($"The area number must not be between [900..999] inclusive. Provided: '{ssn.AreaNumber}'.");
            }

            // Equals either invalidated number.
            if (ssn.Equals(WoolsworthSSN))
            {
                return new ValidationError($"The Social Security Number '{WoolsworthSSN}' has been retired from service.");
            }

            if (ssn.Equals(SampleNumber))
            {
                return new ValidationError($"The Social Security Number '{SampleNumber}' is for documentation purposes only.");
            }

            // It passed all tests.
            return Result.FromSuccess();
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an array.
        /// </summary>
        /// <typeparam name="TNumber">The numeric type to convert into.</typeparam>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        /// <exception cref="OverflowException">Thrown when <typeparamref name="TNumber"/> cannot represent the requested value.</exception>
        public TNumber[] AsArray<TNumber>()
            where TNumber : INumber<TNumber>
        {
            static void Subdivide(short value, TNumber[] number, int count)
            {
                if (value == 0)
                {
                    return;
                }

                for (short x = value; x > 0; x /= 10)
                {
                    number[count--] = TNumber.CreateChecked(x % 10);
                }

                return;
            }

            var number = new TNumber[9];
            Subdivide(SerialNumber, number, count: 8);
            Subdivide(GroupNumber, number, count: 4);
            Subdivide(AreaNumber, number, count: 2);

            return number;
        }
#endif

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is SocialSecurityNumber ssn && Equals(ssn);

        /// <summary>
        /// Compares this instance with another instance of <see cref="SocialSecurityNumber" /> for equality.
        /// </summary>
        /// <param name="other">The instance to compare this one with.</param>
        /// <returns>True if the operands are not equal; otherwise, false.</returns>
        public bool Equals(SocialSecurityNumber other)
            => AreaNumber == other.AreaNumber && GroupNumber == other.GroupNumber && SerialNumber == other.SerialNumber;

        /// <summary>
        /// Returns a string representation of this <see cref="SocialSecurityNumber"/> in General (hyphenated) format.
        /// </summary>
        /// <returns>A string representation of this <see cref="SocialSecurityNumber"/>.</returns>
        public override string ToString()
            => ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns a string representation of this <see cref="SocialSecurityNumber"/> in the requested format.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Format Code</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term>G</term>
        ///         <description>General. Returns a hyphenated format. <c>219-09-9999</c>.</description>
        ///     </item>
        ///     <item>
        ///         <term>H</term>
        ///         <description>Hyphenated. Same as <strong>G</strong>.</description>
        ///     </item>
        ///     <item>
        ///         <term>U</term>
        ///         <description>Unhyphenated. <c>219099999</c>.</description>
        ///     </item>
        ///     <item>
        ///         <term>M</term>
        ///         <description>Masked and hypenated. Shows only the last four digits. <c>***-**-9999</c></description>
        ///     </item>
        ///     <item>
        ///         <term>N</term>
        ///         <description>Masked and (Not) hyphenated. Shows only the last four digits. <c>*****9999</c></description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="format">The format to use to represent the Social Security Number.</param>
        /// <returns>A string representation of this <see cref="SocialSecurityNumber"/>.</returns>
        public string ToString(string format)
            => ToString(format, CultureInfo.CurrentCulture);

        /// <inheritdoc cref="ToString(string)"/>
        /// <param name="format">The format to use to represent the Social Security Number.</param>
        /// <param name="formatProvider">The format provider to use for this operation.</param>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                format = "G";
            }

            return format?.ToUpperInvariant() switch
            {
                "G" or "H" => $"{AreaNumber:D3}-{GroupNumber:D2}-{SerialNumber:D4}",
                "U" => $"{AreaNumber:D3}{GroupNumber:D2}{SerialNumber:D4}",
                "M" => $"***-**-{SerialNumber:D4}",
                "N" => $"*****{SerialNumber:D4}",
                _ => throw new FormatException($"The selected '{format}' format is not not supported."),
            };
        }

        /// <summary>
        /// Compares two instances of <see cref="SocialSecurityNumber" /> for equality.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if the operands are equal; otherwise, false.</returns>
        public static bool operator ==(SocialSecurityNumber left, SocialSecurityNumber right)
            => left.Equals(right);

        /// <summary>
        /// Compares two instances of <see cref="SocialSecurityNumber" /> for inequality.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if the operands are not equal; otherwise, false.</returns>
        public static bool operator !=(SocialSecurityNumber left, SocialSecurityNumber right)
            => !left.Equals(right);

        /// <inheritdoc/>
        public override int GetHashCode()
            => HashCode.Combine(AreaNumber, GroupNumber, SerialNumber);

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
