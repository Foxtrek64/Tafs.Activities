//
//  LengthConstants.cs
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
using System.Text;

namespace Tafs.Activities.FileChunks
{
    /// <summary>
    /// Gets the number of bytes for file length.
    /// </summary>
    public static class LengthConstants
    {
        /// <summary>
        /// Gets the number of bytes in one megabyte.
        /// </summary>
        public const long OneMegabyte = 0xFA00;

        /// <summary>
        /// Gets the number of bytes in two megabytes.
        /// </summary>
        public const long TwoMegabytes = OneMegabyte * 2;

        /// <summary>
        /// Gets the number of bytes in five megabytes.
        /// </summary>
        public const long FiveMegabytes = OneMegabyte * 5;

        /// <summary>
        /// Gets the number of bytes in ten megabytes.
        /// </summary>
        public const long TenMegabytes = OneMegabyte * 10;

        /// <summary>
        /// Gets the number of bytes in one hundred megabytes.
        /// </summary>
        public const long OneHundredMegabytes = TenMegabytes * 10;

        /// <summary>
        /// Gets the number of bytes in one thousand megabytes.
        /// </summary>
        public const long OneGigabyte = OneHundredMegabytes * 100;
    }
}
