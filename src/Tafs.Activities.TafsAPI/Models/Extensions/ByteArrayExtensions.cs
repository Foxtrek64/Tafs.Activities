//
//  ByteArrayExtensions.cs
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
using System.Runtime.InteropServices;

namespace Tafs.Activities.TafsAPI.Models.Extensions
{
    /// <summary>
    /// Provides a set of extensions for converting TAFS API models to and from byte arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Converts the specified struct to a byte array.
        /// </summary>
        /// <typeparam name="TApiModel">The type of <paramref name="obj"/>.</typeparam>
        /// <param name="obj">The struct to convert.</param>
        /// <returns>A byte array.</returns>
        public static byte[] AsByteArray<TApiModel>(this TApiModel obj)
            where TApiModel : struct, IApiModel
        {
            int size = Marshal.SizeOf(obj);

            if (size == 0)
            {
                return []; // Empty array
            }

            byte[] arr = new byte[size];

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(obj, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            return arr;
        }

        /// <summary>
        /// Converts a byte array to the specified API model.
        /// </summary>
        /// <typeparam name="TApiModel">The type of API model to create.</typeparam>
        /// <param name="data">The data to write.</param>
        /// <returns>An API model.</returns>
        public static TApiModel AsApiModel<TApiModel>(this byte[] data)
            where TApiModel : struct, IApiModel
        {
            TApiModel model = default;

            if (data is null || data.Length == 0)
            {
                return model;
            }

            int size = Marshal.SizeOf(model);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.Copy(data, 0, ptr, size);
                model = (TApiModel)Marshal.PtrToStructure(ptr, model.GetType());
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return model;
        }
    }
}
