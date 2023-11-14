//
//  Logger.cs
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
using System.Activities;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using UiPath.Activities.Contracts;

namespace Tafs.Activities.Mediator.Logging
{
    /// <summary>
    /// Provides a logger which writes to the UiPath log stream.
    /// </summary>
    public sealed class Logger : ILogger
    {
        private readonly LogProvider _provider;
        private readonly IWorkflowRuntime _workflowRuntime;
        private readonly Guid _instanceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="provider">A provider that represents context.</param>
        /// <param name="workflowRuntime">The runtime to attach to.</param>
        /// <param name="instanceId">The workflow instance id.</param>
        public Logger
        (
            LogProvider provider,
            IWorkflowRuntime workflowRuntime,
            Guid instanceId
        )
        {
            _provider = provider;
            _workflowRuntime = workflowRuntime;
            _instanceId = instanceId;
        }

        /// <inheritdoc/>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();

            // return _provider.BeginScope(state);
        }

        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel)
        {
            return Enum.IsDefined(typeof(LogLevel), logLevel) && logLevel != LogLevel.None;
        }

        /// <inheritdoc/>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            LogMessage logMessage = new()
            {
                EventType = logLevel switch
                {
                    LogLevel.Trace => TraceEventType.Verbose,
                    LogLevel.Debug => TraceEventType.Verbose,
                    LogLevel.Information => TraceEventType.Information,
                    LogLevel.Warning => TraceEventType.Warning,
                    LogLevel.Error => TraceEventType.Error,
                    LogLevel.Critical => TraceEventType.Critical,
                    _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
                },
                Message = formatter(state, exception)
            };

            _workflowRuntime.Log(logMessage, _instanceId);
        }
    }
}
