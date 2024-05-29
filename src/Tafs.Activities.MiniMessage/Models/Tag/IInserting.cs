//
//  IInserting.cs
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

using JetBrains.Annotations;
using Tafs.Activities.MiniMessage.Models.Text;

namespace Tafs.Activities.MiniMessage.Models.Tag
{
    /// <summary>
    /// A tag that inserts a <see cref="Component"/> into the output.
    /// </summary>
    public interface IInserting : ITag
    {
        /// <summary>
        /// Gets the component this tag produces.
        /// </summary>
        [NotNull]
        IComponent Value { get; }

        /// <summary>
        /// Gets a value indicating whether this tag allows children.
        /// </summary>
        /// <remarks>
        /// If children are not allowed, this tag will be auto-closing and should not
        /// be closed explicitly. In strict mode, a closing tag will be an error. In
        /// lenient mode, the closing tag will be interpreted as literal text.
        /// </remarks>
        bool AllowsChildren { get => true; }
    }
}
