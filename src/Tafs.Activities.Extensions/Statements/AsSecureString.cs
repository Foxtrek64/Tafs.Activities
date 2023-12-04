//
//  AsSecureString.cs
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

using System.Activities;
using System.ComponentModel;
using System.Security;
using Tafs.Activities.SecureStringConverter;

namespace Tafs.Activities.Extensions.Statements
{
    /// <summary>
    /// Converts the provided string into a SecureString.
    /// </summary>
    [DisplayName("String As SecureString")]
    [Description("Convert a string variable to a SecureString. If the input string is null or empty, a null SecureString is returned.")]
    public sealed class AsSecureString : CodeActivity<SecureString?>
    {
        /// <summary>
        /// Gets or sets the input string.
        /// </summary>
        [RequiredArgument]
        [DefaultValue("")]
        [Description("The string to convert into a SecureString.")]
        public InArgument<string> Input { get; set; } = new();

        /// <inheritdoc/>
        protected override SecureString? Execute(CodeActivityContext context)
        {
            var input = Input.Get(context);

            return string.IsNullOrEmpty(input) ? default : input.AsSecureString();
        }
    }
}
