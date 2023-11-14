//
//  SecureStringEqualityComparer.cs
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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace Tafs.Activities.SecureStringConverter
{
    /// <summary>
    /// Defines an <see cref="EqualityComparer{T}"/> for the <see cref="SecureString"/> type.
    /// </summary>
    [PublicAPI]
    public sealed class SecureStringEqualityComparer : IEqualityComparer<SecureString>
    {
        /// <inheritdoc/>
        public bool Equals(SecureString? x, SecureString? y)
        {
            // If both are null, they are equal.
            // If one is null but not both, they are not equal.
            if (x is null)
            {
                return y is null;
            }

            if (y is null)
            {
                // If we hit this point, we know x is not null.
                return false;
            }

            IntPtr leftHandle = IntPtr.Zero;
            IntPtr rightHandle = IntPtr.Zero;
            try
            {
                leftHandle = Marshal.SecureStringToBSTR(x);
                rightHandle = Marshal.SecureStringToBSTR(y);
                int leftLength = Marshal.ReadInt32(leftHandle, -4);
                int rightLength = Marshal.ReadInt32(rightHandle, -4);

                // If they are different lengths, return false.
                if (leftLength != rightLength)
                {
                    return false;
                }

                // Iterate through and ensure each byte is equivalent.
                for (int i = 0; i < leftLength; i++)
                {
                    byte leftByte = Marshal.ReadByte(leftHandle, i);
                    byte rightByte = Marshal.ReadByte(rightHandle, i);

                    // Short circuit as soon as we stop being equal.
                    if (leftByte != rightByte)
                    {
                        return false;
                    }
                }

                // We've iterated through every set of bytes and they all match.
                return true;
            }
            finally
            {
                Marshal.ZeroFreeBSTR(leftHandle);
                Marshal.ZeroFreeBSTR(rightHandle);
            }
        }

        /// <inheritdoc/>
        public int GetHashCode([DisallowNull] SecureString obj)
        {
            var hashCode = default(HashCode);

            foreach (byte i in obj)
            {
                hashCode.Add(i);
            }

            return hashCode.GetHashCode();
        }
    }
}
