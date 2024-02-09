//
//  ReverseChunkIterator.cs
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
    internal class ReverseChunkIterator : IEnumerable<string>, IAsyncEnumerable<string>
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
        public IEnumerator<string> GetEnumerator()
            => ChunkIterator.GetChunkEnumerator(_reverseChunks, _mmf);

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <inheritdoc/>
        public IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            => ChunkIterator.GetAsyncChunkEnumerator(_reverseChunks, _mmf, cancellationToken);
    }
}
