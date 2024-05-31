//
//  ParseError.cs
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
using Remora.Results;

namespace Tafs.Activities.Results.Extensions.Errors
{
    /// <summary>
    /// Represents an error from attempting to parse a string into a different type.
    /// </summary>
    /// <typeparam name="TValue">The type that we attempted to parse to.</typeparam>
    /// <param name="Value">The value that failed to be parsed.</param>
    /// <param name="Reason">Optional. A reason <paramref name="Value"/> failed to be parsed.</param>
    /// <param name="MessageBuilder">A function to format the message, accepting <paramref name="Value"/> and <paramref name="Reason"/> and returning a message string.</param>
    public sealed record class ParseError<TValue>(string Value, string? Reason, Func<string, string?, string> MessageBuilder)
        : ResultError(MessageFormatter(Value, Reason))
    {
        private static string MessageFormatter(string value, string? reason)
        {
            reason = string.IsNullOrWhiteSpace(reason) ? string.Empty : $": {reason}";
            return $"{value} was not in the expected format to convert into an instance of {typeof(TValue).Name}{reason}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseError{TValue}"/> class.
        /// </summary>
        /// <param name="value">The value that failed to be parsed.</param>
        public ParseError(string value)
            : this(value, null, MessageFormatter)
        {
        }
    }
}
