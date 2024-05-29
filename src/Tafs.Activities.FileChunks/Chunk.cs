//
//  Chunk.cs
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

using System.IO.MemoryMappedFiles;

namespace Tafs.Activities.FileChunks
{
    /// <summary>
    /// Represents a chunk of a <see cref="MemoryMappedFile"/>.
    /// </summary>
    /// <param name="Offset">The offset in bytes.</param>
    /// <param name="Size">The size of the chunk in bytes.</param>
    /// <param name="Content">The contents of the chunk as text.</param>
    public sealed record class Chunk
    (
        long Offset,
        long Size,
        string Content
    )
    {
        /// <summary>
        /// Gets the offset in bytes to the text in the original file.
        /// </summary>
        public long Offset { get; } = Offset;

        /// <summary>
        /// Gets the length of the content in bytes.
        /// </summary>
        public long Size { get; } = Size;

        /// <summary>
        /// Gets the contents of the chunk as text.
        /// </summary>
        public string Content { get; } = Content;
    }
}
