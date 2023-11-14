//
//  Mediator.cs
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
using System.Threading;
using System.Threading.Tasks;
using Tafs.Activities.Mediator.EventHandlers;
using Tafs.Activities.Mediator.Events;
using Tafs.Activities.Mediator.Exceptions;

namespace Tafs.Activities.Mediator
{
    /// <summary>
    /// Defines a generic mediator.
    /// </summary>
    public sealed class Mediator : IMediator
    {
        private readonly Dictionary<object, object> _voidRequestHandlers = new();
        private readonly Dictionary<object, object> _returningRequestHandlers = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediator"/> class.
        /// </summary>
        /// <param name="voidRequestHandlers">A mapping of requests to their handlers.</param>
        /// <param name="returningRequestHandlers">A mapping of returning requests to their handlers.</param>
        public Mediator(Dictionary<object, object> voidRequestHandlers, Dictionary<object, object> returningRequestHandlers)
        {
            _voidRequestHandlers = voidRequestHandlers;
            _returningRequestHandlers = returningRequestHandlers;
        }

        /// <inheritdoc/>
        public async ValueTask Publish<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : notnull, IRequest
        {
            if (!_voidRequestHandlers.TryGetValue(request, out var handlerObject))
            {
                throw new MissingMessageHandlerException<Unit>(request);
            }

            var handler = handlerObject as IRequestHandler<TRequest>;

            await handler!.Handle(request, cancellationToken);
        }

        /// <inheritdoc/>
        public ValueTask<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : notnull, IRequest<TResponse>
        {
            if (!_returningRequestHandlers.TryGetValue(request, out var handlerObject))
            {
                throw new MissingMessageHandlerException<TResponse>(request);
            }

            var handler = handlerObject as IRequestHandler<TRequest, TResponse>;

            return handler!.Handle(request, cancellationToken);
        }

        /// <summary>
        /// Adds an <see cref="IRequest"/> to the handlers.
        /// </summary>
        /// <typeparam name="TRequest">The type of request.</typeparam>
        /// <typeparam name="TRequestHandler">The type of handler.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="requestHandler">The request handler.</param>
        public void AddVoidRequestHandler<TRequest, TRequestHandler>(TRequest request, TRequestHandler requestHandler)
            where TRequest : notnull, IRequest
            where TRequestHandler : IRequestHandler<TRequest, Unit>
        {
            _voidRequestHandlers.Add(request, requestHandler);
        }

        /// <summary>
        /// Adds an <see cref="IRequest{TResponse}"/> to the handlers.
        /// </summary>
        /// <typeparam name="TRequest">The type of request.</typeparam>
        /// <typeparam name="TResponse">The type of response.</typeparam>
        /// <typeparam name="TRequestHandler">The type of handler.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="requestHandler">The handler.</param>
        public void AddRequestHandler<TRequest, TResponse, TRequestHandler>(TRequest request, TRequestHandler requestHandler)
            where TRequest : notnull, IRequest<TResponse>
            where TRequestHandler : IRequestHandler<TRequest, TResponse>
        {
            _returningRequestHandlers.Add(request, requestHandler);
        }
    }
}
