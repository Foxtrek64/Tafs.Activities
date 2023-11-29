//
//  AreEqual.cs
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
using System.Collections.Generic;
using System.ComponentModel;

namespace Tafs.Activities.Extensions.Conditions
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Ensures the provided <typeparamref name="TEntity"/> values are equal using the provided
    /// <see cref="EqualityComparer"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to compare.</typeparam>
    [Description("Accepts two variables of the same type to evaluate them for equality.")]
    public sealed class AreEqual<TEntity> : CodeActivity<bool>
    {
        /// <summary>
        /// Gets or sets the expected value.
        /// </summary>
        [RequiredArgument]
        [Description("The expected value. Alternatively, just a left input.")]

        public InArgument<TEntity> Expected { get; set; }

        /// <summary>
        /// Gets or sets the actual value.
        /// </summary>
        [RequiredArgument]
        [Description("The actual alue. Alternatively, just a right input.")]
        public InArgument<TEntity> Actual { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IEqualityComparer{T}"/> to use for this operation.
        /// </summary>
        [Description("Optional. Provide an EqualityComparer to assist with getting expected results.")]
        public InArgument<IEqualityComparer<TEntity>> EqualityComparer { get; set; } = EqualityComparer<TEntity>.Default;

        /// <inheritdoc/>
        protected override bool Execute(CodeActivityContext context)
        {
            TEntity left = Expected.Get(context);
            TEntity right = Actual.Get(context);
            IEqualityComparer<TEntity> equalityComparer = EqualityComparer.Get(context);

            return equalityComparer.Equals(left, right);
        }
    }
}
