//
//  LogProvider.cs
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
using Microsoft.Extensions.Logging;
using UiPath.Activities.Contracts;
using Context = System.Activities.ActivityContext;

namespace Tafs.Activities.Mediator.Logging
{
    /// <summary>
    /// Defines a <see cref="ILoggerProvider"/> that pipes events through UiPath.
    /// </summary>
    [ProviderAlias("UiPath")]
    public sealed class LogProvider : ILoggerProvider
    {
        private const string OriginalFormatPropertyName = "{OriginalFormat}";
        private const string ScopePropertyName = "Scope";

        private readonly Context _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogProvider"/> class.
        /// </summary>
        /// <param name="context">The context for this operation.</param>
        public LogProvider(Context context)
        {
            _context = context;
        }

        /// <inheritdoc cref="IDisposable"/>
        public static IDisposable? BeginScope<T>(T state)
        {
            return new LoggerScope();
        }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(this, _context.GetExtension<IWorkflowRuntime>(), _context.WorkflowInstanceId);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}
