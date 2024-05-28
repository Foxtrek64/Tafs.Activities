//
//  INode.cs
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

using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tafs.Activities.MiniMessage.Models.Tree
{
    /// <summary>
    /// Defines a node in a MiniMessage parse tree.
    /// </summary>
    [PublicAPI]
    internal interface INode
    {
        /// <summary>
        /// Gets the parent node or null if this is the top-most node.
        /// </summary>
        INode? Parent { get; }

        /// <summary>
        /// Gets children of this node.
        /// </summary>
        /// <returns>A list of child nodes.</returns>
        [NotNull]
        IReadOnlyList<INode> GetChildren();

        /// <summary>
        /// The root node of a parse.
        /// </summary>
        public interface IRoot : INode
        {
            /// <summary>
            /// Gets the original provided message which produced this node.
            /// </summary>
            [NotNull]
            string Input { get; }
        }
    }
}
