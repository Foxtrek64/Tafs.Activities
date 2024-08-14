//
//  ArrayExtensions.cs
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
using Tafs.Activities.Results.Extensions.Errors;

namespace Tafs.Activities.Finance.Extensions
{
    /// <summary>
    /// Provides a collection of extension methods for <see cref="Array"/>.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Ensures all items in the input <paramref name="array"/> are compliant with the
        /// conditions defined in the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The underlying type of the array.</typeparam>
        /// <param name="array">The array to test.</param>
        /// <param name="expected">A string value used when rendering an error to define an example of expected values.</param>
        /// <param name="predicate">A function against which to test each member of the <paramref name="array"/>.</param>
        /// <returns>A result describing the success or failure of the action.</returns>
        public static Result AllMatch<T>(this T[] array, string expected, Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                T item = array[i];
                if (!predicate(item))
                {
                    return new ValidationError(expected, item?.ToString() ?? "[null]", $"Invalid character at position '{i}': '{item}'.");
                }
            }

            return Result.FromSuccess();
        }
    }
}
