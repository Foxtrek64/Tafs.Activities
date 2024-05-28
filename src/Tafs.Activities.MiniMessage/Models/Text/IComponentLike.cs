//
//  IComponentLike.cs
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
using JetBrains.Annotations;

namespace Tafs.Activities.MiniMessage.Models.Text
{
    /// <summary>
    /// Something that can be represented as a <see cref="IComponent"/>.
    /// </summary>
    [PublicAPI]
    [Obsolete("Do we even need a component-like? What is this used for?")]
    public interface IComponentLike
    {
        /// <summary>
        /// Converts a list of <see cref="IComponentLike"/>s to a list of <see cref="IComponent"/>.
        /// </summary>
        /// <param name="likes">The component-likes.</param>
        /// <returns>The components.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="likes"/> is null.</exception>
        /// <exception cref="NullReferenceException">Thrown when any element of <paramref name="likes"/> is null.</exception>
        static IReadOnlyList<IComponent> AsComponents([NotNull] List<IComponentLike> likes)
            => AsComponents(likes, null);

        /// <summary>
        /// Converts a list of <see cref="IComponentLike"/>s to a list of <see cref="IComponent"/>.
        /// </summary>
        /// <param name="likes">The component-likes.</param>
        /// <param name="filter">The component filter.</param>
        /// <returns>The components.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="likes"/> is null.</exception>
        /// <exception cref="NullReferenceException">Thrown when any element of <paramref name="likes"/> is null.</exception>
        static IReadOnlyList<IComponent> AsComponents([NotNull] List<IComponentLike> likes, Func<IComponent, bool>? filter)
        {
            if (likes is null || likes.Count == 0)
            {
                throw new ArgumentNullException(nameof(likes));
            }

            List<IComponent> components = new();

            if (!likes.Any())
            {
                return components.AsReadOnly();
            }

            foreach (var like in likes)
            {
                if (like is null)
                {
                    throw new NullReferenceException(nameof(like));
                }

                IComponent component = like.AsComponent();

                if (filter is null || filter.Invoke(component))
                {
                    components.Add(component);
                }
            }

            return components.AsReadOnly();
        }

        /// <summary>
        /// Gets a representation of this <see cref="IComponentLike"/> as a <see cref="IComponent"/>.
        /// </summary>
        /// <returns>A <see cref="IComponent"/>.</returns>
        [Pure]
        [NotNull]
        abstract IComponent AsComponent();
    }
}
