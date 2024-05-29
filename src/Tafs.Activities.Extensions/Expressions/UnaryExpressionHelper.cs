//
//  UnaryExpressionHelper.cs
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
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Tafs.Activities.Extensions.Extensions;

namespace Tafs.Activities.Extensions.Expressions
{
    /// <summary>
    /// Helper class for <see cref="UnaryExpression"/>s.
    /// </summary>
    public sealed class UnaryExpressionHelper
    {
        /// <inheritdoc cref="OnGetArgumentsBase{TOperand}(CodeActivityMetadata, Argument, ArgumentDirection)"/>
        public static void OnGetArguments<TOperand>(CodeActivityMetadata metadata, InArgument<TOperand> operand)
            => OnGetArgumentsBase<TOperand>(metadata, operand, ArgumentDirection.In);

        /// <inheritdoc cref="OnGetArgumentsBase{TOperand}(CodeActivityMetadata, Argument, ArgumentDirection)"/>
        public static void OnGetArguments<TOperand>(CodeActivityMetadata metadata, InOutArgument<TOperand> operand)
            => OnGetArgumentsBase<TOperand>(metadata, operand, ArgumentDirection.InOut);

        /// <summary>
        /// Binds the metadata for the argument.
        /// </summary>
        /// <typeparam name="TOperand">The type of the argument.</typeparam>
        /// <param name="metadata">The metadata.</param>
        /// <param name="operand">The argument.</param>
        /// <param name="argumentDirection">Gets the direction of <paramref name="operand"/>.</param>
        public static void OnGetArgumentsBase<TOperand>(CodeActivityMetadata metadata, Argument operand, ArgumentDirection argumentDirection)
        {
            RuntimeArgument operandArgument = new("Operand", typeof(TOperand), argumentDirection, true);
            metadata.Bind(operand, operandArgument);

            metadata.SetArgumentsCollection(
                [
                    operandArgument
                ]);
        }

        /// <summary>
        /// Generates a <see cref="System.Linq"/> delegate.
        /// </summary>
        /// <typeparam name="TOperand">The type of the argument.</typeparam>
        /// <typeparam name="TResult">The return type of the operation.</typeparam>
        /// <param name="operatorType">The type of expression.</param>
        /// <param name="function">The resulting <see cref="Func{T1, T2, TResult}"/>.</param>
        /// <param name="validationError">If the operation failed, the error.</param>
        /// <returns><see langword="true"/> if the operation was successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGenerateLinqDelegate<TOperand, TResult>
        (
            ExpressionType operatorType,
            [NotNullWhen(true)] out Func<TOperand, TResult>? function,
            [NotNullWhen(false)] out ValidationError? validationError
        )
        {
            function = null;
            validationError = null;

            ParameterExpression operandParameter = Expression.Parameter(typeof(TOperand), "operand");
            try
            {
                UnaryExpression unaryExpression = Expression.MakeUnary(operatorType, operandParameter, typeof(TResult));
                Expression<Func<TOperand, TResult>> lambdaExpression = Expression.Lambda<Func<TOperand, TResult>>(unaryExpression, operandParameter);
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
