//
//  SPDX-FileName: ChunkIterator.cs
//  SPDX-FileCopyrightText: Copyright (c) TAFS, LLC.
//  SPDX-License-Identifier: MIT
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;

namespace Tafs.Activities.FileChunks
{
    /// <summary>
    /// Provides an iterator for itering through chunks in a file.
    /// </summary>
    public class ChunkIterator : IEnumerable<Chunk>, IAsyncEnumerable<Chunk>, IDisposable
    {
        private readonly FileInfo _file;
        private readonly long _fileLength;
        private readonly long _chunkLength;
        private readonly MemoryMappedFile _memoryMappedFile;
        private readonly Dictionary<long, long> _chunks;

        private bool _disposed = false;
        private ReverseChunkIterator? _reverseIterator;

        /// <summary>
        /// Gets the number of chunks managed by this chunk iterator.
        /// </summary>
        public int Length => _chunks.Count;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkIterator"/> class.
        /// </summary>
        /// <param name="filePath">The file to parse.</param>
        /// <param name="chunkLength">The chunk length in bytes.</param>
        public ChunkIterator(string filePath, long chunkLength = LengthConstants.TwoMegabytes)
        {
            _file = new FileInfo(filePath);
            _fileLength = _file.Length < chunkLength ? _file.Length : chunkLength;
            _chunkLength = chunkLength;

            _memoryMappedFile = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open, "log");

            int chunkCounts = (int)(_chunkLength / _file.Length);
            long remainingBytes = _chunkLength % _file.Length;

            int size = chunkCounts + (remainingBytes > 0 ? 1 : 0);

            _chunks = new(size);

            for (int i = 0; i < size; i++)
            {
                long offset = i * _chunkLength;

                if (offset > _fileLength)
                {
                    break;
                }

                long length = offset + _chunkLength > _fileLength
                    ? _fileLength - offset
                    : _chunkLength;

                _chunks.Add(offset, length);
            }
        }

        /// <inheritdoc/>
        public IEnumerator<Chunk> GetEnumerator()
            => GetChunkEnumerator(_chunks, _memoryMappedFile);

        /// <summary>
        /// Gets a reversed chunk enumerator.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/>.</returns>
        public IEnumerable<Chunk> Reverse()
        {
            _reverseIterator ??= new ReverseChunkIterator(_chunks, _memoryMappedFile);
            return _reverseIterator;
        }

        /// <summary>
        /// Gets a reversed chunk enumerator.
        /// </summary>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/>.</returns>
        public IAsyncEnumerable<Chunk> ReverseAsync()
        {
            _reverseIterator ??= new ReverseChunkIterator(_chunks, _memoryMappedFile);
            return _reverseIterator;
        }

        /// <summary>
        /// Enumerates the chunks, returning all text in that chunk as a string.
        /// </summary>
        /// <param name="chunks">The chunks to enumerate.</param>
        /// <param name="memoryMappedFile">The MMF to inspect.</param>
        /// <returns>An <see cref="IEnumerator{T}"/> for the chunk text.</returns>
        internal static IEnumerator<Chunk> GetChunkEnumerator(Dictionary<long, long> chunks, MemoryMappedFile memoryMappedFile)
        {
            foreach (var (offset, length) in chunks)
            {
                using var streamer = memoryMappedFile.CreateViewStream(offset, length);

                var chunkBytes = new byte[streamer.Length];
                streamer.Read(chunkBytes, 0, chunkBytes.Length);
                var lines = Encoding.UTF8.GetString(chunkBytes);
                var chunk = new Chunk(offset, length, lines);

                yield return chunk;
            }
        }

        /// <inheritdoc/>
        public IAsyncEnumerator<Chunk> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            => GetAsyncChunkEnumerator(_chunks, _memoryMappedFile, cancellationToken);

        /// <summary>
        /// Enumerates the chunks asynchronously, returning all text in that text as a string.
        /// </summary>
        /// <param name="chunks">The chunks to enumerate.</param>
        /// <param name="memoryMappedFile">The MMF to inspect.</param>
        /// <param name="cancellationToken">The cancellation token for this operation.</param>
        /// <returns>An <see cref="IAsyncEnumerator{T}"/> for the chunk text.</returns>
        internal static async IAsyncEnumerator<Chunk> GetAsyncChunkEnumerator
        (
            Dictionary<long, long> chunks,
            MemoryMappedFile memoryMappedFile,
            CancellationToken cancellationToken = default
        )
        {
            foreach (var (offset, length) in chunks)
            {
                using var streamer = memoryMappedFile.CreateViewStream(offset, length);

                var chunkBytes = new byte[streamer.Length];
                _ = await streamer.ReadAsync(chunkBytes, cancellationToken);
                var lines = Encoding.UTF8.GetString(chunkBytes);
                var chunk = new Chunk(offset, length, lines);

                yield return chunk;
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                GC.SuppressFinalize(this);
                _memoryMappedFile.Dispose();
            }
        }
    }
}
