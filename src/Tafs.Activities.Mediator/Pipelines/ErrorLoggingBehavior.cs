//
//  ErrorLoggingBehavior.cs
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
using Microsoft.Extensions.Logging;
using Tafs.Activities.Mediator.Events;

namespace Tafs.Activities.Mediator.Pipelines
{
    /// <summary>
    /// Provides logging as a pipeline behavior.
    /// </summary>
    /// <typeparam name="TMessage">The message.</typeparam>
    /// <typeparam name="TResponse">The response.</typeparam>
    public sealed class ErrorLoggingBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
        where TMessage : notnull, IMessage
    {
        private readonly ILogger<ErrorLoggingBehavior<TMessage, TResponse>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLoggingBehavior{TMessage, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">A logger for this instance.</param>
        public ErrorLoggingBehavior(ILogger<ErrorLoggingBehavior<TMessage, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next(message, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling message of type {meessageType}", message.GetType().Name);
                throw;
            }
        }
    }
}
