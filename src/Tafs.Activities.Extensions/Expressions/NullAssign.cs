//
//  NullAssign.cs
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
using System.Activities.Validation;
using System.Linq.Expressions;

namespace Tafs.Activities.Extensions.Expressions
{
    /// <summary>
    /// A code activity which performs an assignment operation only if the receiving parameter is null.
    /// </summary>
    /// <remarks>
    /// Programmatically represents <see cref="Left"/> <c>??=</c> <see cref="Right"/>.
    /// </remarks>
    /// <typeparam name="TLeft">The operand to modify.</typeparam>
    /// <typeparam name="TRight">The operand with which to modify <typeparamref name="TLeft"/>.</typeparam>
    public sealed class NullAssign<TLeft, TRight> : CodeActivity<TLeft>
        where TRight : notnull, TLeft
    {
        private static Func<TLeft, TRight, TLeft>? operationFunction = null!;

        /// <summary>
        /// Gets or sets the instance which will receive the assignment.
        /// </summary>
        [RequiredArgument]
        public InOutArgument<TLeft> Left { get; set; } = new();

        /// <summary>
        /// Gets or sets the value on the right side of the operation.
        /// </summary>
        public InArgument<TRight> Right { get; set; } = new();

        /// <inheritdoc/>
        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            BinaryExpressionHelper.OnGetArguments(metadata, Left, Right);

            NullAssign<TLeft, TRight>.EnsureOperationFunction(metadata, ref operationFunction, ExpressionType.Assign);
        }

        private static void EnsureOperationFunction
        (
            CodeActivityMetadata metadata,
            ref Func<TLeft, TRight, TLeft>? operationFunction,
            ExpressionType operatorType
        )
        {
            if (operationFunction == null )
            {
                if (!BinaryExpressionHelper.TryGenerateLinqDelegate
                    (
                        operatorType,
                        out operationFunction,
                        out ValidationError? validationError
                    )
                   )
                {
                    metadata.AddValidationError(validationError);
                }
            }
        }

        /// <inheritdoc/>
        protected override TLeft Execute(CodeActivityContext context)
        {
            TLeft leftValue = Left.Get(context);
            TRight rightValue = Right.Get(context);

            return leftValue ?? operationFunction!.Invoke(leftValue, rightValue);
        }
    }
}
