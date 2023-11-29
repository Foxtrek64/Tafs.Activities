//
//  ExceptionExtensions.cs
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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tafs.Activities.Extensions.Extensions
{
    /// <summary>
    /// Provides a set of extensions for <see cref="Exception"/> types.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Returns a boolean indicating whether the exception is fatal (cannot be recovered from).
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>True if the exception is fatal; otherwise, false.</returns>
        public static bool IsFatal(this Exception? exception)
        {
            while (exception is not null)
            {
                if (exception is OutOfMemoryException)
                {
                    return true;
                }

                if (exception is TypeInitializationException or TargetInvocationException)
                {
                    exception = exception.InnerException;
                }
                else if (exception is AggregateException aggregateException)
                {
                    foreach (var ex in aggregateException.InnerExceptions)
                    {
                        return ex.IsFatal();
                    }

                    break;
                }
                else
                {
                    break;
                }
            }

            return false;
        }
    }
}
