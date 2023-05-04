//
//  Extensions.cs
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
using System.Runtime.InteropServices;
using System.Security;

namespace Tafs.Activities.SecureStringConverter
{
    /// <summary>
    /// Provides extension methods for converting to and from <see cref="SecureString"/>.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Marshals a <paramref name="secureString"/> to a clear-text string.
        /// </summary>
        /// <param name="secureString">The <see cref="SecureString"/> to convert.</param>
        /// <returns>A clear-string representation of the <paramref name="secureString"/>.</returns>
        public static string AsClearString(this SecureString secureString)
        {
            IntPtr valuePtr = IntPtr.Zero;

            try
            {
                valuePtr = Marshal.SecureStringToBSTR(secureString);
                return Marshal.PtrToStringBSTR(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(valuePtr);
            }
        }

        /// <summary>
        /// Converts the provided <see cref="string"/> value into a <see cref="SecureString"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="SecureString"/> with its contents set to the provided string value.</returns>
        public static SecureString AsSecureString(this string value)
        {
            var secureString = new SecureString();
            Array.ForEach(value.ToCharArray(), secureString.AppendChar);
            secureString.MakeReadOnly();
            return secureString;
        }

        /// <summary>
        /// Enumerates the raw bytes of the provided <paramref name="secureString"/>.
        /// </summary>
        /// <param name="secureString">The SecureString to enumerate.</param>
        /// <returns>An enumerator for the SecureString.</returns>
        public static IEnumerator<byte> GetEnumerator(this SecureString secureString)
        {
            IntPtr marshalHandle = IntPtr.Zero;
            try
            {
                marshalHandle = Marshal.SecureStringToBSTR(secureString);
                int length = Marshal.ReadInt32(marshalHandle, -4);
                for (int i = 0; i < length; i++)
                {
                    yield return Marshal.ReadByte(marshalHandle, i);
                }
            }
            finally
            {
                Marshal.ZeroFreeBSTR(marshalHandle);
            }
        }
    }
}