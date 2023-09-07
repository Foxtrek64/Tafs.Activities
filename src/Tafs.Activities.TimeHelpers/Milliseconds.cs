//
//  Milliseconds.cs
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

namespace Tafs.Activities.TimeHelpers
{
    /// <summary>
    /// Provides static helper methods for converting other time units to milliseconds.
    /// </summary>
    public static class Milliseconds
    {
        /// <summary>
        /// Converts a specified number of <paramref name="seconds"/> to milliseconds.
        /// </summary>
        /// <param name="seconds">The number of seconds.</param>
        /// <returns>An integer indicating the number of seconds.</returns>
        public static int FromSeconds(int seconds)
        {
            return seconds * 1000;
        }

        public static int FromMinutes(int minutes)
        {
            return minutes * 60 * 1000;
        }

        public static int FromHours(int hours)
        {
            return hours * 60 * 60 * 1000;
        }

        public static int FromTimeSpan(TimeSpan timeSpan)
        {
            return (int)timeSpan.TotalMilliseconds;
        }
    }
}