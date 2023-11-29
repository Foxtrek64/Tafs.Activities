//
//  AreNotSame.cs
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

using System.Activities;
using System.ComponentModel;

namespace Tafs.Activities.Extensions.Conditions
{
    /// <summary>
    /// Ensures that two objects are not the same instance.
    /// </summary>
    [Description("Ensures that two objects are not the same instance.")]
    public sealed class AreNotSame : CodeActivity<bool>
    {
        /// <summary>
        /// Gets or sets the expected object instance.
        /// </summary>
        [RequiredArgument]
        [Description("The expected object instance.")]
        public InArgument<object?> Expected { get; set; } = new(constValue: null);

        /// <summary>
        /// Gets or sets the actual object instance.
        /// </summary>
        [RequiredArgument]
        [Description("The actual object instance.")]
        public InArgument<object?> Actual { get; set; } = new(constValue: null);

        /// <inheritdoc/>
        protected override bool Execute(CodeActivityContext context)
        {
            object? left = Expected.Get(context);
            object? right = Actual.Get(context);

            return !object.ReferenceEquals(left, right);
        }
    }
}
