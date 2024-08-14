//
//  SPDX-FileName: Chunk.cs
//  SPDX-FileCopyrightText: Copyright (c) TAFS, LLC.
//  SPDX-License-Identifier: MIT
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
