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
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Remora.Results;
using Tafs.Activities.Finance.Extensions;
using Vogen;

namespace Tafs.Activities.Finance.Models
{
    /// <summary>
    /// Represents a 9-digit United States social security number.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    [ValueObject<string>]
    public readonly partial struct SocialSecurityNumber : IEquatable<SocialSecurityNumber>
    {
        private const string SsnRegexDefinition = @"^(?<area>\d{3})(?<separator>[ -]?)(?<group>\d{2})\2(?<serial>\d{4})$";

#if NET7_0_OR_GREATER
        [GeneratedRegex(SsnRegexDefinition, RegexOptions.Compiled)]
        private static partial Regex SsnRegex();
#else
        private static readonly Regex _ssnRegex = new(SsnRegexDefinition, RegexOptions.Compiled);

        private static Regex SsnRegex() => _ssnRegex;
#endif

        /// <inheritdoc cref="Zero"/>
        [Obsolete("Use Zero")]
        public static readonly SocialSecurityNumber Default = Zero;

        /// <summary>
        /// Gets a default <see cref="SocialSecurityNumber"/> of 000-00-0000.
        /// </summary>
        /// <remarks>
        /// This social security number is invalid.
        /// </remarks>
        public static readonly SocialSecurityNumber Zero = new("000-00-0000");

        /// <summary>
        /// Gets a <see cref="SocialSecurityNumber"/> used as a sample for advertisements and documentation by the Social Security Administration.
        /// </summary>
        /// <remarks>219-09-9999.</remarks>
        public static readonly SocialSecurityNumber SampleNumber = new("219-09-9999");

        /// <inheritdoc cref="WoolsworthNumber"/>
        [Obsolete("Use Woolsworth Number")]
        public static readonly SocialSecurityNumber WoolsworthSSN = WoolsworthNumber;

        /// <summary>
        /// Gets a decommissioned <see cref="SocialSecurityNumber"/> once issued to an employee of Woolsworth Wallets.
        /// </summary>
        /// <remarks>078-05-1120.</remarks>
        /// <seealso href="https://www.ssa.gov/history/ssn/misused.html"/>
        public static readonly SocialSecurityNumber WoolsworthNumber = new("078-05-1120");

        private static string NormalizeInput(string input)
            => input.Trim();

        /// <summary>
        /// Validates a Social Security Number to ensure none of the following are met:
        /// <list type="bullet">
        ///     <item>Contains all zeroes in any specific group (000-##-####, ###-00-####, or ###-##-0000).</item>
        ///     <item>Begins with '666'.</item>
        ///     <item>Begins with any value from '900-999'.</item>
        /// </list>
        /// </summary>
        /// <param name="input">The Social Security Number to validate.</param>
        /// <returns>A result indicating whether the validation succeeded.</returns>
        private static Validation Validate(string input)
        {
            var match = SsnRegex().Match(input);
            if (match.Success)
            {
                var area = int.Parse(match.Groups["area"].Value);
                var group = int.Parse(match.Groups["group"].Value);
                var serial = int.Parse(match.Groups["serial"].Value);

                // Not all zeroes
                if (area == 0)
                {
                    return Validation.Invalid("The area number cannot equal '000'");
                }

                if (group == 0)
                {
                    return Validation.Invalid("The group number cannot equal '00'");
                }

                if (serial == 0)
                {
                    return Validation.Invalid("The serial number cannot equal '0000'");
                }

                // Range validation
                if (area == 666)
                {
                    return Validation.Invalid("The area number cannot equal '666'");
                }

                if (area is >= 900 and <= 999)
                {
                    return Validation.Invalid("Area number cannot be between 900 and 999 (inclusive)");
                }

                return Validation.Ok;
            }

            return Validation.Invalid("Input string must consist of only numbers 0-9 and optional hyphens and spaces between sections.");
        }

        /// <inheritdoc cref="TryParseResult(string)"/>
        /// <param name="ssn">The social security number to validate.</param>
        /// <param name="validate">Unused. Value is ignored.</param>
        [Obsolete("Use version without boolean.")]
        public static Result<SocialSecurityNumber> TryParseResult(string ssn, bool validate)
            => TryParseResult(ssn);

        /// <summary>
        /// Attempts to parse the provided string into an instance of <see cref="SocialSecurityNumber"/>.
        /// </summary>
        /// <param name="ssn">The social security number to validate.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the <see cref="SocialSecurityNumber"/> or a failure result.</returns>
        public static Result<SocialSecurityNumber> TryParseResult(string ssn)
            => TryFrom(ssn).AsResult();

        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an int array.
        /// </summary>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        public int[] AsIntArray()
        {
#if NET7_0_OR_GREATER
            return AsArray<int>();
#else
            var number = new int[9];
            for (int i = 0; i < Value.Length; i++)
            {
                number[i] = (int)char.GetNumericValue(Value[i]);
            }

            return number;
#endif
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
            string value = ToString("U");
            var number = new TNumber[9];
            for (int i = 0; i < 9; i++)
            {
                number[i] = TNumber.CreateChecked(char.GetNumericValue(value[i]));
            }

            return number;
        }
#endif

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
        ///         <term>S</term>
        ///         <description>Space. Returns a spaced format. <c>219 09 9999</c>,</description>
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

            var match = SsnRegex().Match(_value);
            var areaNumber = match.Groups["area"].Value;
            var groupNumber = match.Groups["group"].Value;
            var serialNumber = match.Groups["serial"].Value;

            return format?.ToUpperInvariant() switch
            {
                "G" or "H" => $"{areaNumber}-{groupNumber}-{serialNumber}",
                "S" => $"{areaNumber} {groupNumber} {serialNumber}",
                "U" => $"{areaNumber}{groupNumber}{serialNumber}",
                "M" => $"***-**-{serialNumber}",
                "N" => $"*****{serialNumber}",
                _ => throw new FormatException($"The selected '{format}' format is not not supported."),
            };
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        /// <inheritdoc/>
        public bool Equals(SocialSecurityNumber other)
        {
            return AsIntArray().SequenceEqual(other.AsIntArray());
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return ToString("U").GetHashCode();
        }
    }
}
