//
//  SPDX-FileName: ReverseChunkIterator.cs
//  SPDX-FileCopyrightText: Copyright (c) TAFS, LLC.
//  SPDX-License-Identifier: MIT
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Threading;

namespace Tafs.Activities.FileChunks
{
    /// <summary>
    /// Iterates chunks in reverse.
    /// </summary>
    internal class ReverseChunkIterator : IEnumerable<Chunk>, IAsyncEnumerable<Chunk>
    {
        private readonly Dictionary<long, long> _reverseChunks;
        private readonly MemoryMappedFile _mmf;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseChunkIterator"/> class.
        /// </summary>
        /// <param name="chunks">The chunks represented by a pairing of offset to length.</param>
        /// <param name="memoryMappedFile">The file to read.</param>
        internal ReverseChunkIterator(Dictionary<long, long> chunks, MemoryMappedFile memoryMappedFile)
        {
            _reverseChunks = new(chunks.Reverse());
            _mmf = memoryMappedFile;
        }

        /// <inheritdoc/>
        public IEnumerator<Chunk> GetEnumerator()
            => ChunkIterator.GetChunkEnumerator(_reverseChunks, _mmf);

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <inheritdoc/>
        public IAsyncEnumerator<Chunk> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            => ChunkIterator.GetAsyncChunkEnumerator(_reverseChunks, _mmf, cancellationToken);
    }
}
