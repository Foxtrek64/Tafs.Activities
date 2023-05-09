//
//  SecureStringConverterTests.cs
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

using System.Security;
using Xunit;

namespace Tafs.Activities.SecureStringConverter.Tests
{
    /// <summary>
    /// Performs tests on the SecureString converters.
    /// </summary>
    public sealed class SecureStringConverterTests
    {
        private readonly string _clearString = "MyP@ssw0rd!";
        private readonly SecureString _testSecureString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecureStringConverterTests"/> class.
        /// </summary>
        public SecureStringConverterTests()
        {
            _testSecureString = new SecureString();
            foreach (var character in _clearString)
            {
                _testSecureString.AppendChar(character);
            }

            _testSecureString.MakeReadOnly();
        }

        /// <summary>
        /// Ensures converting to a SecureString works.
        /// </summary>
        [Fact]
        public void ToSecureString()
        {
            var secureString = _clearString.AsSecureString();

            Assert.Equal(_testSecureString, secureString, new SecureStringEqualityComparer());
            Assert.Equal(_clearString, secureString.AsClearString());
        }

        /// <summary>
        /// Ensures converting to a clear string works.
        /// </summary>
        [Fact]
        public void ToClearString()
        {
            Assert.Equal(_clearString, _testSecureString.AsClearString());
        }
    }
}
