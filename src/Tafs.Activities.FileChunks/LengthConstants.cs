//
//  SPDX-FileName: LengthConstants.cs
//  SPDX-FileCopyrightText: Copyright (c) TAFS, LLC.
//  SPDX-License-Identifier: MIT
//

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
