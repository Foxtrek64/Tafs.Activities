//
//  RethrowException.cs
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

namespace Tafs.Activities.Extensions.Statements
{
    /// <summary>
    /// Rethrows the provided exception outside of a catch block without resetting the stack trace.
    /// </summary>
    public sealed class RethrowException : CodeActivity
    {
        /// <summary>
        /// Gets or sets the exception to rethrow.
        /// </summary>
        [RequiredArgument]
        public InArgument<Exception> Exception { get; set; } = new();

        /// <inheritdoc/>
        protected override void Execute(CodeActivityContext context)
        {
            var ex = Exception.Get(context);
            System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw();

            // Unreachable code. Informs compiler that the flow never leaves the block.
            throw ex;
        }
    }
}
