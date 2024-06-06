//
//  SocialSecurityNumberExtensions.cs
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

using Tafs.Activities.Finance.Models;

namespace Tafs.Activities.Finance.Extensions
{
    /// <summary>
    /// Provides a set of extensions for <see cref="SocialSecurityNumber"/>.
    /// </summary>
    public static class SocialSecurityNumberExtensions
    {
        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an array.
        /// </summary>
        /// <param name="ssn">The social security number to process.</param>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        public static byte[] AsByteArray(this SocialSecurityNumber ssn)
        {
            static void Subdivide(short value, byte[] number, int count)
            {
                if (value == 0)
                {
                    return;
                }

                for (short x = value; x > 0; x /= 10)
                {
                    number[count--] = (byte)(x % 10);
                }

                return;
            }

            var number = new byte[9];
            Subdivide(ssn.SerialNumber, number, count: 8);
            Subdivide(ssn.GroupNumber, number, count: 4);
            Subdivide(ssn.AreaNumber, number, count: 2);

            return number;
        }

        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an array.
        /// </summary>
        /// <param name="ssn">The social security number to process.</param>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        public static sbyte[] AsSByteArray(this SocialSecurityNumber ssn)
        {
            static void Subdivide(short value, sbyte[] number, int count)
            {
                if (value == 0)
                {
                    return;
                }

                for (short x = value; x > 0; x /= 10)
                {
                    number[count--] = (sbyte)(x % 10);
                }

                return;
            }

            var number = new sbyte[9];
            Subdivide(ssn.SerialNumber, number, count: 8);
            Subdivide(ssn.GroupNumber, number, count: 4);
            Subdivide(ssn.AreaNumber, number, count: 2);

            return number;
        }

        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an array.
        /// </summary>
        /// <param name="ssn">The social security number to process.</param>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        public static short[] AsInt16Array(this SocialSecurityNumber ssn)
        {
            static void Subdivide(short value, short[] number, int count)
            {
                if (value == 0)
                {
                    return;
                }

                for (short x = value; x > 0; x /= 10)
                {
                    number[count--] = (short)(x % 10);
                }

                return;
            }

            var number = new short[9];
            Subdivide(ssn.SerialNumber, number, count: 8);
            Subdivide(ssn.GroupNumber, number, count: 4);
            Subdivide(ssn.AreaNumber, number, count: 2);

            return number;
        }

        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an array.
        /// </summary>
        /// <param name="ssn">The social security number to process.</param>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        public static ushort[] AsUInt16Array(this SocialSecurityNumber ssn)
        {
            static void Subdivide(short value, ushort[] number, int count)
            {
                if (value == 0)
                {
                    return;
                }

                for (short x = value; x > 0; x /= 10)
                {
                    number[count--] = (ushort)(x % 10);
                }

                return;
            }

            var number = new ushort[9];
            Subdivide(ssn.SerialNumber, number, count: 8);
            Subdivide(ssn.GroupNumber, number, count: 4);
            Subdivide(ssn.AreaNumber, number, count: 2);

            return number;
        }

        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an array.
        /// </summary>
        /// <param name="ssn">The social security number to process.</param>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        public static int[] AsInt32Array(this SocialSecurityNumber ssn)
        {
            static void Subdivide(short value, int[] number, int count)
            {
                if (value == 0)
                {
                    return;
                }

                for (short x = value; x > 0; x /= 10)
                {
                    number[count--] = (int)(x % 10);
                }

                return;
            }

            var number = new int[9];
            Subdivide(ssn.SerialNumber, number, count: 8);
            Subdivide(ssn.GroupNumber, number, count: 4);
            Subdivide(ssn.AreaNumber, number, count: 2);

            return number;
        }

        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an array.
        /// </summary>
        /// <param name="ssn">The social security number to process.</param>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        public static uint[] AsUInt32Array(this SocialSecurityNumber ssn)
        {
            static void Subdivide(short value, uint[] number, int count)
            {
                if (value == 0)
                {
                    return;
                }

                for (short x = value; x > 0; x /= 10)
                {
                    number[count--] = (uint)(x % 10);
                }

                return;
            }

            var number = new uint[9];
            Subdivide(ssn.SerialNumber, number, count: 8);
            Subdivide(ssn.GroupNumber, number, count: 4);
            Subdivide(ssn.AreaNumber, number, count: 2);

            return number;
        }

        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an array.
        /// </summary>
        /// <param name="ssn">The social security number to process.</param>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        public static long[] AsInt64Array(this SocialSecurityNumber ssn)
        {
            static void Subdivide(short value, long[] number, int count)
            {
                if (value == 0)
                {
                    return;
                }

                for (short x = value; x > 0; x /= 10)
                {
                    number[count--] = (long)(x % 10);
                }

                return;
            }

            var number = new long[9];
            Subdivide(ssn.SerialNumber, number, count: 8);
            Subdivide(ssn.GroupNumber, number, count: 4);
            Subdivide(ssn.AreaNumber, number, count: 2);

            return number;
        }

        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an array.
        /// </summary>
        /// <param name="ssn">The social security number to process.</param>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        public static ulong[] AsUInt64Array(this SocialSecurityNumber ssn)
        {
            static void Subdivide(short value, ulong[] number, int count)
            {
                if (value == 0)
                {
                    return;
                }

                for (short x = value; x > 0; x /= 10)
                {
                    number[count--] = (ulong)(x % 10);
                }

                return;
            }

            var number = new ulong[9];
            Subdivide(ssn.SerialNumber, number, count: 8);
            Subdivide(ssn.GroupNumber, number, count: 4);
            Subdivide(ssn.AreaNumber, number, count: 2);

            return number;
        }

        /// <summary>
        /// Gets this <see cref="SocialSecurityNumber"/> as an array.
        /// </summary>
        /// <param name="ssn">The social security number to process.</param>
        /// <returns>This <see cref="SocialSecurityNumber"/> as an array.</returns>
        public static object[] AsObjectArray(this SocialSecurityNumber ssn)
        {
            static void Subdivide(short value, object[] number, int count)
            {
                if (value == 0)
                {
                    return;
                }

                for (short x = value; x > 0; x /= 10)
                {
                    number[count--] = (object)(x % 10);
                }

                return;
            }

            var number = new object[9];
            Subdivide(ssn.SerialNumber, number, count: 8);
            Subdivide(ssn.GroupNumber, number, count: 4);
            Subdivide(ssn.AreaNumber, number, count: 2);

            return number;
        }
    }
}
