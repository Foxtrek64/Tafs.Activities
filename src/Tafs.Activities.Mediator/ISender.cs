//
//  ISender.cs
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
using System.Threading;
using System.Threading.Tasks;
using Tafs.Activities.Mediator.Events;
using Tafs.Activities.Mediator.Exceptions;

namespace Tafs.Activities.Mediator
{
    /// <summary>
    /// Mediator instance for sending requests.
    /// </summary>
    public interface ISender
    {
        /// <summary>
        /// Sends a request.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The incoming request.</param>
        /// <param name="cancellationToken">A cancellation token for this request.</param>
        /// <exception cref="ArgumentNullException">Thrown if the message is null.</exception>
        /// <exception cref="MissingMessageHandlerException">Thrown if no handler is registered.</exception>
        /// <returns>A <see cref="ValueTask{TResult}"/> containing the response.</returns>
        ValueTask<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : notnull, IRequest<TResponse>;
    }
}
