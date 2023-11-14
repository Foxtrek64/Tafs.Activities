//
//  MissingMessageHandlerException.cs
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
using Tafs.Activities.Mediator.Events;

namespace Tafs.Activities.Mediator.Exceptions
{
    /// <summary>
    /// Defines an exception that is thrown when Mediator receives messages
    /// that have no registered handlers.
    /// </summary>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public sealed class MissingMessageHandlerException<TResponse> : Exception
    {
        /// <summary>
        /// Gets the request which was sent.
        /// </summary>
        public IRequest<TResponse>? MediatorRequest { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingMessageHandlerException{TResponse}"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public MissingMessageHandlerException(IRequest<TResponse>? request)
            : base("No handler registered for message type: " + request?.GetType().FullName ?? "Unknown")
        {
            MediatorRequest = request;
        }
    }
}
