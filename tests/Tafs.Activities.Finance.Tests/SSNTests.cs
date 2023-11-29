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
        public static readonly IReadOnlyList<object[]> SSNToIntArray = new List<object[]>()
        {
            new object[] { SocialSecurityNumber.Default, new int[9] },
            new object[] { SocialSecurityNumber.SampleNumber, new int[] { 2, 1, 9, 0, 9, 9, 9, 9, 9 } },
            new object[] { SocialSecurityNumber.WoolsworthSSN, new int[] { 0, 7, 8, 0, 5, 1, 1, 2, 0 } }
        };

        /// <summary>
        /// Maps a <see cref="SocialSecurityNumber"/> to a <see cref="string"/>.
        /// </summary>
        public static readonly IReadOnlyList<object[]> SSNToString = new List<object[]>()
        {
            new object[] { SocialSecurityNumber.Default, "000-00-0000", false },
            new object[] { SocialSecurityNumber.SampleNumber, "219-09-9999", false },
            new object[] { SocialSecurityNumber.WoolsworthSSN, "078-05-1120", false }
        };

        /// <summary>
        /// Ensures <see cref="SocialSecurityNumber.AsArray"/> outputs correct value.
        /// </summary>
        /// <param name="input">The <see cref="SocialSecurityNumber"/>.</param>
        /// <param name="expected">An int array representing the <see cref="SocialSecurityNumber"/>.</param>
        [Theory]
        [MemberData(nameof(SSNToIntArray))]
        public void OutputsCorrectArray(SocialSecurityNumber input, int[] expected)
        {
            Assert.Equal(expected, input.AsArray());
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
