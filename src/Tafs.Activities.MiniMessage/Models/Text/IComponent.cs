//
//  IComponent.cs
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
using System.ComponentModel;
using System.Text;
using JetBrains.Annotations;

namespace Tafs.Activities.MiniMessage.Models.Text
{
    /// <summary>
    /// A <see cref="IComponent"/> is an immutable object that represents how text
    /// is displayed.
    ///
    /// Components can be thought of as a combination of:
    /// <list type="bullet">
    /// <item>The message the Component wants to display; and</item>
    /// <item>The <see cref="IStyle"/> of that message.</item>
    /// </list>
    ///
    /// Components can be seriailized to and deserialized from other
    /// formats via the use of <see cref="IComponentSerializer"/>s.
    /// </summary>
    /// <seealso cref="KeybindComponent"/>
    /// <seealso cref="TextComponent"/>
    /// <seealso cref="TranslatableComponent"/>
    /// <seealso cref="LinearComponents"/>
    public interface IComponent :
        ComponentBuilderApplicable,
        IComponentLike,
        Examinable,
        StyleGetter,
        StyleSetter<IComponent>
    {
        /// <summary>
        /// Joins <see cref="IComponent"/>s using the configuration in <paramref name="config"/>.
        /// </summary>
        /// <param name="config">The join configuration.</param>
        /// <param name="components">The components.</param>
        /// <returns>The resulting component.</returns>
        /// <seealso cref="JoinConfiguration.NoSeparators()"/>
        /// <seealso cref="JoinConfiguration.Separator(ComponentLike)"/>
        /// <seealso cref="JoinConfiguration.Separators(ComponentLike, ComponentLike)"/>
        [Pure]
        [NotNull]
        static abstract IComponent Join([NotNull] JoinConfiguration config, [NotNull] params IComponentLike[] components);


        // static abstract Collector<>
    }
}
