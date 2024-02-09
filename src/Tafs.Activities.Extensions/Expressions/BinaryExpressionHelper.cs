//
//  BinaryExpressionHelper.cs
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
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Tafs.Activities.Extensions.Extensions;

namespace Tafs.Activities.Extensions.Expressions
{
    /// <summary>
    /// Helpers for binary operations.
    /// </summary>
    internal static class BinaryExpressionHelper
    {
        /// <inheritdoc cref="OnGetArgumentsBase{TLeft, TRight}(CodeActivityMetadata, Argument, ArgumentDirection, Argument, ArgumentDirection)"/>
        public static void OnGetArguments<TLeft, TRight>(CodeActivityMetadata metadata, InArgument<TLeft> left, InArgument<TRight> right)
            => OnGetArgumentsBase<TLeft, TRight>(metadata, left, ArgumentDirection.In, right, ArgumentDirection.In);

        /// <inheritdoc cref="OnGetArgumentsBase{TLeft, TRight}(CodeActivityMetadata, Argument, ArgumentDirection, Argument, ArgumentDirection)"/>
        public static void OnGetArguments<TLeft, TRight>(CodeActivityMetadata metadata, InOutArgument<TLeft> left, InArgument<TRight> right)
            => OnGetArgumentsBase<TLeft, TRight>(metadata, left, ArgumentDirection.InOut, right, ArgumentDirection.In);

        /// <inheritdoc cref="OnGetArgumentsBase{TLeft, TRight}(CodeActivityMetadata, Argument, ArgumentDirection, Argument, ArgumentDirection)"/>
        public static void OnGetArguments<TLeft, TRight>(CodeActivityMetadata metadata, InArgument<TLeft> left, InOutArgument<TRight> right)
            => OnGetArgumentsBase<TLeft, TRight>(metadata, left, ArgumentDirection.In, right, ArgumentDirection.InOut);

        /// <inheritdoc cref="OnGetArgumentsBase{TLeft, TRight}(CodeActivityMetadata, Argument, ArgumentDirection, Argument, ArgumentDirection)"/>
        public static void OnGetArguments<TLeft, TRight>(CodeActivityMetadata metadata, InOutArgument<TLeft> left, InOutArgument<TRight> right)
            => OnGetArgumentsBase<TLeft, TRight>(metadata, left, ArgumentDirection.InOut, right, ArgumentDirection.InOut);

        /// <summary>
        /// Binds metadata when getting arguments.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left argument.</typeparam>
        /// <typeparam name="TRight">The type of the right argument.</typeparam>
        /// <param name="metadata">The metadata.</param>
        /// <param name="left">The left argument.</param>
        /// <param name="leftDirection">The direction of <paramref name="left"/>.</param>
        /// <param name="right">The right argument.</param>
        /// <param name="rightDirection">The direction of <paramref name="right"/>.</param>
        internal static void OnGetArgumentsBase<TLeft, TRight>
        (
            CodeActivityMetadata metadata,
            Argument left,
            ArgumentDirection leftDirection,
            Argument right,
            ArgumentDirection rightDirection
        )
        {
            RuntimeArgument rightArgument = new("Right", typeof(TRight), rightDirection, true);
            metadata.Bind(right, rightArgument);

            RuntimeArgument leftArgument = new("Left", typeof(TLeft), leftDirection, true);
            metadata.Bind(left, leftArgument);

            metadata.SetArgumentsCollection(
            [
                rightArgument,
                leftArgument
            ]);
        }

        /// <summary>
        /// Generates a <see cref="System.Linq"/> delegate.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left argument.</typeparam>
        /// <typeparam name="TRight">The type of the right argument.</typeparam>
        /// <typeparam name="TResult">The return type of the operation.</typeparam>
        /// <param name="operatorType">The type of expression.</param>
        /// <param name="function">The resulting <see cref="Func{T1, T2, TResult}"/>.</param>
        /// <param name="validationError">If the operation failed, the error.</param>
        /// <returns><see langword="true"/> if the operation was successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGenerateLinqDelegate<TLeft, TRight, TResult>
        (
            ExpressionType operatorType,
            [NotNullWhen(true)] out Func<TLeft, TRight, TResult>? function,
            [NotNullWhen(false)] out ValidationError? validationError
        )
        {
            function = null;
            validationError = null;

            ParameterExpression leftParameter = Expression.Parameter(typeof(TLeft), "left");
            ParameterExpression rightParameter = Expression.Parameter(typeof(TRight), "right");

            try
            {
                BinaryExpression binaryExpression = Expression.MakeBinary(operatorType, leftParameter, rightParameter);
                Expression<Func<TLeft, TRight, TResult>> lambdaExpression = Expression.Lambda<Func<TLeft, TRight, TResult>>(binaryExpression, leftParameter, rightParameter);
                function = lambdaExpression.Compile();

                return true;
            }
            catch (Exception ex)
            {
                if (ex.IsFatal())
                {
                    throw;
                }

                validationError = new ValidationError(ex.Message);
                return false;
            }
        }
    }
}
