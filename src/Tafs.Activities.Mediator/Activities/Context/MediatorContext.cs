//
//  MediatorContext.cs
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
using Microsoft.Extensions.Logging;

namespace Tafs.Activities.Mediator.Activities.Context
{
    /// <summary>
    /// Defines the context shared with child activities of the Mediator.
    /// </summary>
    public sealed class MediatorContext : IDisposable
    {
        /// <summary>
        /// Gets the logger for the MediatorContext.
        /// </summary>
        public ILogger Logger { get; init; }

        /// <summary>
        /// Gets the Mediator for the MediatorContext.
        /// </summary>
        public IMediator Mediator { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatorContext"/> class.
        /// </summary>
        /// <param name="logger">A logger for this instance.</param>
        /// <param name="mediator">The mediator for this instance.</param>
        public MediatorContext(ILogger logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
