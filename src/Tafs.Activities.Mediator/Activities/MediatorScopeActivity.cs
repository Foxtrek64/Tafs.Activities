//
//  MediatorScopeActivity.cs
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
using System.Activities.Statements;
using System.Collections.Generic;
using System.ComponentModel;
using Tafs.Activities.Mediator.Activities.Context;
using Tafs.Activities.Mediator.Logging;

#pragma warning disable SA1206 // Declaration keywords should follow order
namespace Tafs.Activities.Mediator.Activities
{
    /// <summary>
    /// Provides a logical scope for all Mediated activities.
    /// </summary>
    [DisplayName("Mediator Scope")]
    [Description("Provides a logical scope for mediated activities.")]
    public sealed class MediatorScopeActivity : NativeActivity
    {
        /// <summary>
        /// Gets or sets the body of the activity scope.
        /// </summary>
        [Browsable(false)]
        public ActivityAction<MediatorContext> Body { get; set; }

        private MediatorContext? _mediatorContext;
        private LogProvider? _logProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatorScopeActivity"/> class.
        /// </summary>
        public MediatorScopeActivity()
        {
            Body = new ActivityAction<MediatorContext>
            {
                Argument = new DelegateInArgument<MediatorContext>(nameof(MediatorContext)),
                Handler = new TryCatch { DisplayName = "Scope", Try = new Sequence() { DisplayName = "Body" }, Finally = new Sequence() }
            };
        }

        /// <inheritdoc/>
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
        }

        /// <inheritdoc/>
        protected override void Execute(NativeActivityContext context)
        {
            try
            {
                _logProvider = new(context);

                _mediatorContext = new MediatorContext(_logProvider.CreateLogger(nameof(MediatorContext)), new Mediator(new(), new()));

                if (Body is not null)
                {
                    context.ScheduleAction(Body, _mediatorContext, OnCompleted, OnFaulted);
                }
            }
            catch (Exception)
            {
                CleanupContext();
                throw;
            }
        }

        private void CleanupContext()
        {
            _mediatorContext?.Dispose();

            _mediatorContext = null;
        }

        private void OnFaulted(NativeActivityFaultContext faultContext, Exception propogatedException, ActivityInstance propogatedFrom)
        {
            faultContext.CancelChildren();
            CleanupContext();
        }

        private void OnCompleted(NativeActivityContext context, ActivityInstance completedInstance)
        {
            CleanupContext();
        }
    }
}
