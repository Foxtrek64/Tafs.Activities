//
//  IPublisher.cs
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

using System.Threading;
using System.Threading.Tasks;
using Tafs.Activities.Mediator.Events;
using Tafs.Activities.Mediator.Exceptions;

namespace Tafs.Activities.Mediator
{
    /// <summary>
    /// A mediator instance for publishing requests.
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// Publishes a request (fire-and-forget).
        /// </summary>
        /// <typeparam name="TRequest">The type of request.</typeparam>
        /// <param name="request">The request. Must return <see cref="Unit"/>.</param>
        /// <param name="cancellationToken">A cancellation token for this operation.</param>
        /// <exception cref="MissingMessageHandlerException">Thrown when there is no handler for this request.</exception>
        /// <returns>An awaitable <see cref="ValueTask"/>.</returns>
        ValueTask Publish<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest;
    }
}
