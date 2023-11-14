//
//  LogFactory.cs
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
using Microsoft.Extensions.Logging;

namespace Tafs.Activities.Mediator.Logging
{
    /// <summary>
    /// Provides a <see cref="ILoggerFactory"/> for UiPath logging.
    /// </summary>
    public sealed class LogFactory : ILoggerFactory
    {
        private readonly List<ILoggerProvider> _loggerProviders = new();
        private readonly LogProvider _provider;

        private readonly object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="LogFactory"/> class.
        /// </summary>
        /// <param name="provider">A provider for this instance.</param>
        public LogFactory(LogProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc/>
        public void AddProvider(ILoggerProvider provider)
        {
            lock (_lock)
            {
                _loggerProviders.Add(provider);
            }
        }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            return _provider.CreateLogger(categoryName);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}
