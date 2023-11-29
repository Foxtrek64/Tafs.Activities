﻿//
//  IOptional.cs
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
#if NET461_OR_GREATER

using JetBrains.Annotations;

namespace Remora.Rest.Core
{
    /// <summary>
    /// Defines basic functionality for an optional.
    /// </summary>
    [PublicAPI]
    public interface IOptional
    {
        /// <summary>
        /// Gets a value indicating whether the optional contains a value.
        /// </summary>
        bool HasValue { get; }

        /// <summary>
        /// Determines whether the optional has a defined value; that is, whether it both has a value and that value is non-null.
        /// </summary>
        /// <returns><see langword="true"/> if the optional hasa value that is non-null; otherwise, <see langword="false"/>.</returns>
        bool IsDefined();
    }
}
#endif
