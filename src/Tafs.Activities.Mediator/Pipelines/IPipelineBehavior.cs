//
//  IPipelineBehavior.cs
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

namespace Tafs.Activities.Mediator.Pipelines
{
    /// <summary>
    /// The base pipeline marker interface.
    /// </summary>
    /// <typeparam name="TMessage">The message to handle.</typeparam>
    /// <typeparam name="TResponse">The expected response type.</typeparam>
    public interface IPipelineBehavior<TMessage, TResponse>
        where TMessage : notnull, IMessage
    {
        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="next">The next event in the sequence.</param>
        /// <param name="cancellationToken">A cancellation token for this operation.</param>
        /// <returns>A <see cref="ValueTask{TResult}"/> containing the response.</returns>
        ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken);
    }
}
