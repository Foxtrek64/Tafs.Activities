//
//  SSNTests.cs
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
using Remora.Results;
using Tafs.Activities.Finance.Extensions;
using Tafs.Activities.Finance.Models;
using Tafs.Activities.Results.Extensions.Errors;
using Xunit;

namespace Tafs.Activities.Finance.Tests
{
    /// <summary>
    /// Provides unit tests for the <see cref="SocialSecurityNumber"/> type.
    /// </summary>
    public class SSNTests
    {
        /// <summary>
        /// Maps a <see cref="SocialSecurityNumber"/> to an integer array.
        /// </summary>
        public static readonly IReadOnlyList<object[]> SSNToIntArray =
        [
            [SocialSecurityNumber.Default, new int[9]],
            [SocialSecurityNumber.SampleNumber, new int[] { 2, 1, 9, 0, 9, 9, 9, 9, 9 }],
            [SocialSecurityNumber.WoolsworthSSN, new int[] { 0, 7, 8, 0, 5, 1, 1, 2, 0 }]
        ];

        /// <summary>
        /// Maps a <see cref="SocialSecurityNumber"/> to a short array.
        /// </summary>
        public static readonly IReadOnlyList<object[]> SSNToShortArray =
        [
            [SocialSecurityNumber.Default, new short[9]],
            [SocialSecurityNumber.SampleNumber, new short[] { 2, 1, 9, 0, 9, 9, 9, 9, 9 }],
            [SocialSecurityNumber.WoolsworthSSN, new short[] { 0, 7, 8, 0, 5, 1, 1, 2, 0 }]
        ];

        /// <summary>
        /// Maps a <see cref="SocialSecurityNumber"/> to a byte array.
        /// </summary>
        public static readonly IReadOnlyList<object[]> SSNToByteArray =
        [
            [SocialSecurityNumber.Default, new byte[9]],
            [SocialSecurityNumber.SampleNumber, new byte[] { 2, 1, 9, 0, 9, 9, 9, 9, 9 }],
            [SocialSecurityNumber.WoolsworthSSN, new byte[] { 0, 7, 8, 0, 5, 1, 1, 2, 0 }]
        ];

        /// <summary>
        /// Gets a list of <see cref="SocialSecurityNumbers"/>.
        /// </summary>
        public static readonly IReadOnlyList<object[]> SocialSecurityNumbers =
        [
            [SocialSecurityNumber.Default],
            [SocialSecurityNumber.SampleNumber],
            [SocialSecurityNumber.WoolsworthSSN]
        ];

        /// <summary>
        /// Maps a <see cref="SocialSecurityNumber"/> to a <see cref="string"/>.
        /// </summary>
        public static readonly IReadOnlyList<object[]> SSNToString =
        [
            [SocialSecurityNumber.Default, "000-00-0000", false],
            [SocialSecurityNumber.SampleNumber, "219-09-9999", false],
            [SocialSecurityNumber.WoolsworthSSN, "078-05-1120", false]
        ];

        /// <summary>
        /// Ensures <see cref="SocialSecurityNumber.AsArray"/> outputs correct values.
        /// </summary>
        /// <param name="input">The <see cref="SocialSecurityNumber"/>.</param>
        /// <param name="expected">An int array representing the <see cref="SocialSecurityNumber"/>.</param>
        [Theory]
        [MemberData(nameof(SSNToIntArray))]
        public void OutputsCorrectIntArray(SocialSecurityNumber input, int[] expected)
        {
            Assert.Equal(expected, input.AsArray<int>());
        }

        /// <summary>
        /// Ensures <see cref="SocialSecurityNumber.AsArray{TNumber}()"/> outputs correct values.
        /// </summary>
        /// <param name="input">The <see cref="SocialSecurityNumber"/>.</param>
        /// <param name="expected">A short array representing the <see cref="SocialSecurityNumber"/>.</param>
        [Theory]
        [MemberData(nameof(SSNToShortArray))]
        public void OutputsCorrectShortArray(SocialSecurityNumber input, short[] expected)
        {
            Assert.Equal(expected, input.AsArray<short>());
        }

        /// <summary>
        /// Ensures <see cref="SocialSecurityNumber.AsArray{TNumber}()"/> outputs correct values.
        /// </summary>
        /// <param name="input">The <see cref="SocialSecurityNumber"/>.</param>
        [Theory]
        [MemberData(nameof(SocialSecurityNumbers))]
        public void BitArrayThrows(SocialSecurityNumber input)
        {
            if (!Array.TrueForAll(input.AsArray<int>(), it => it is 0 or 1))
            {
                Assert.Throws<OverflowException>(input.AsArray<Bit>);
            }
        }

        /// <summary>
        /// Ensures <see cref="SocialSecurityNumber.FromString(string)"/> correctly parses the SSN.
        /// </summary>
        /// <param name="expected">The expected SSN.</param>
        /// <param name="value">A string value for parsing.</param>
        /// <param name="valid">A value indicating whether the SSN should be valid.</param>
        [Theory]
        [MemberData(nameof(SSNToString))]
        public void EnsureParse(SocialSecurityNumber expected, string value, bool valid)
        {
            Result<SocialSecurityNumber> parseResult = SocialSecurityNumber.FromString(value);

            if (valid)
            {
                Assert.Null(parseResult.Error);
                Assert.True(parseResult.IsDefined(out var ssn));
                Assert.Equal(expected, ssn);
            }
            else
            {
                Assert.False(parseResult.IsSuccess);
                Assert.IsType<ValidationError>(parseResult.Error);
                Assert.NotNull(parseResult.Error);
            }
        }
    }
}
