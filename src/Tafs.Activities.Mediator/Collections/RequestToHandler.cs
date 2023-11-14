//
//  RequestToHandler.cs
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
using System.Diagnostics.CodeAnalysis;
using Tafs.Activities.Mediator.EventHandlers;
using Tafs.Activities.Mediator.Events;

namespace Tafs.Activities.Mediator.Collections
{
    /// <summary>
    /// Provides a collection to map a specific request type to a specific handler.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public sealed class RequestToHandler<TRequest, TResponse> : IDisposable
        where TRequest : notnull, IRequest<TResponse>
    {
        private readonly Dictionary<TRequest, IRequestHandler<TRequest, TResponse>> _storage = new();

        /// <inheritdoc cref="Dictionary{TKey, TValue}.Add(TKey, TValue)"/>
        public void Add(TRequest request, IRequestHandler<TRequest, TResponse> handler) => _storage.Add(request, handler);

        /// <inheritdoc cref="Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/>
        public bool TryGetValue(TRequest request, [MaybeNullWhen(false)] out IRequestHandler<TRequest, TResponse> handler) => _storage.TryGetValue(request, out handler);

        /// <inheritdoc/>
        public void Dispose()
        {
            _storage.Clear();
        }
    }
}
