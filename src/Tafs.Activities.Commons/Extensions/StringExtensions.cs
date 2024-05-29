//
//  StringExtensions.cs
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
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Tafs.Activities.Commons.Extensions
{
    /// <summary>
    /// Provides a set of extensions for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another
        /// specified string according the type of search to use for the specified string.
        /// </summary>
        /// <param name="str">The string performing the replace method.</param>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string replace all occurrences of <paramref name="oldValue"/>.
        /// If value is equal to <c>null</c>, than all occurrences of <paramref name="oldValue"/> will be removed from the <paramref name="str"/>.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>A string that is equivalent to the current string except that all instances of <paramref name="oldValue"/> are replaced with <paramref name="newValue"/>.
        /// If <paramref name="oldValue"/> is not found in the current instance, the method returns the current instance unchanged.</returns>
        [DebuggerStepThrough]
        [PublicAPI]
        public static string Replace
        (
            this string str,
            string oldValue,
            string newValue,
            StringComparison comparisonType
        )
        {
            // Check inputs.
            if (str == null)
            {
                // Same as original .NET C# string.Replace behavior.
                throw new ArgumentNullException(nameof(str));
            }

            if (oldValue == null)
            {
                // Same as original .NET C# string.Replace behavior.
                throw new ArgumentNullException(nameof(oldValue));
            }

            if (oldValue.Length == 0)
            {
                // Same as original .NET C# string.Replace behavior.
                throw new ArgumentException("String cannot be of zero length.");
            }

            if (str.Length == 0)
            {
                // Same as original .NET C# string.Replace behavior.
                return str;
            }

            // Prepare string builder for storing the processed string.
            // Note: StringBuilder has a better performance than String by 30-40%.
            StringBuilder resultStringBuilder = new(str.Length);

            // Analyze the replacement: replace or remove.
            bool isReplacementNullOrEmpty = string.IsNullOrEmpty(newValue);

            // Replace all values.
            const int valueNotFound = -1;
            int foundAt;
            int startSearchFromIndex = 0;
            while ((foundAt = str.IndexOf(oldValue, startSearchFromIndex, comparisonType)) != valueNotFound)
            {
                // Append all characters until the found replacement.
                int charsUntilReplacment = foundAt - startSearchFromIndex;
                bool isNothingToAppend = charsUntilReplacment == 0;
                if (!isNothingToAppend)
                {
                    resultStringBuilder.Append(str, startSearchFromIndex, charsUntilReplacment);
                }

                // Process the replacement.
                if (!isReplacementNullOrEmpty)
                {
                    resultStringBuilder.Append(newValue);
                }

                // Prepare start index for the next search.
                // This prevents an infinitel loop replacing the same value repeatedly.
                // For example: if oldValue == "EXAMPLE", newValue == "example", comparisonType == "any ignore case"
                // "EXAMPLE" to "example" to "example" to "example" …
                startSearchFromIndex = foundAt + oldValue.Length;
                if (startSearchFromIndex == str.Length)
                {
                    // We've reached the end of the input string.
                    return resultStringBuilder.ToString();
                }
            }

            // More text after the last replacement exists.
            // Append it to the end of the relaced string.
            int charsUntilStringEnd = str.Length - startSearchFromIndex;
            resultStringBuilder.Append(str, startSearchFromIndex, charsUntilStringEnd);

            return resultStringBuilder.ToString();
        }

        /// <summary>
        /// Converts the provided set of runes into a string.
        /// </summary>
        /// <param name="runes">The runes to concatenate.</param>
        /// <returns>A string representing the set of runes.</returns>
        [DebuggerStepThrough]
        [PublicAPI]
        public static string AsString(this IEnumerable<Rune> runes)
            => string.Create(runes.Count(), runes, static (buffer, runes) =>
            {
                foreach (var rune in runes)
                {
                    var charsWritten = rune.EncodeToUtf16(buffer);
                    buffer = buffer[charsWritten..];
                }
            });
    }
}
