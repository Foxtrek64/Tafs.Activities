//
//  InArgumentExtensions.cs
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

using System.Activities;
using System.Diagnostics.CodeAnalysis;

namespace Tafs.Activities.Extensions.Extensions
{
    /// <summary>
    /// Provides a set of extensions for <see cref="InArgument{T}"/>.
    /// </summary>
    public static class InArgumentExtensions
    {
        /// <summary>
        /// Gets the value from the <paramref name="argument"/> or if null, the provided <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">The type of argument to return.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="context">Context for getting the value.</param>
        /// <param name="defaultValue">The fallback value to return if the argument is null.</param>
        /// <returns>The value of the <paramref name="argument"/>; otherwise, <paramref name="defaultValue"/>.</returns>
        [return: MaybeNull]
        public static T Get<T>(this InArgument<T> argument, ActivityContext context, T defaultValue)
        {
            return argument.Get(context) ?? defaultValue;
        }
    }
}
