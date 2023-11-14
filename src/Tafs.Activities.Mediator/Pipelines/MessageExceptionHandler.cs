//
//  MessageExceptionHandler.cs
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

namespace Tafs.Activities.Mediator.Pipelines
{
    /// <summary>
    /// Provides a base type for exception handlers.
    /// </summary>
    /// <typeparam name="TMessage">The message.</typeparam>
    /// <typeparam name="TResponse">The response.</typeparam>
    /// <typeparam name="TException">The exception type.</typeparam>
    public abstract class MessageExceptionHandler<TMessage, TResponse, TException> : IPipelineBehavior<TMessage, TResponse>
        where TMessage : notnull, IMessage
        where TException : Exception
    {
        /// <inheritdoc/>
        public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next(message, cancellationToken);
            }
            catch (TException ex)
            {
                var result = await Handle(message, ex, cancellationToken);

                if (!result._handled)
                {
                    throw;
                }

                return result._response;
            }
        }

        /// <summary>
        /// A function to handle the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="cancellationToken">A cancellation token for this operation.</param>
        /// <returns>A <see cref="ValueTask{TResult}"/> containing the response.</returns>
        protected abstract ValueTask<ExceptionHandlingResult<TResponse>> Handle(TMessage message, TException exception, CancellationToken cancellationToken);

        /// <inheritdoc cref="ExceptionHandlingResult{TResponse}.NotHandled"/>
        protected static readonly ExceptionHandlingResult<TResponse> NotHandled = ExceptionHandlingResult<TResponse>.NotHandled;

        /// <inheritdoc cref="ExceptionHandlingResult{TResponse}.Handled(TResponse)"/>
        protected static ExceptionHandlingResult<TResponse> Handled(TResponse response) => ExceptionHandlingResult<TResponse>.Handled(response);
    }
}
