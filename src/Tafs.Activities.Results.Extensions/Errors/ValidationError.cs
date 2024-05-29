//
//  ValidationError.cs
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

using Remora.Results;

namespace Tafs.Activities.Results.Extensions.Errors
{
    /// <summary>
    /// Represents an error that occurred as a result of validation.
    /// </summary>
    /// <param name="Message">The reason validation failed.</param>
    public sealed record class ValidationError(string Message) : ResultError(Message)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <param name="comment">An optional comment to provide additional detail.</param>
        public ValidationError
        (
            string expected,
            string actual,
            string? comment = null
        )
            : this($"'{expected}' does not equal '{actual}'. {comment ?? string.Empty}".Trim())
        {
        }
    }
}
