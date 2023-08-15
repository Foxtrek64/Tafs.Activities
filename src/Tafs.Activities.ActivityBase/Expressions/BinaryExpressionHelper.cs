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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tafs.Activities.ActivityBase.Expressions
{
    /// <summary>
    /// Provides a set of extension methods for building expressions.
    /// </summary>
    internal static class BinaryExpressionHelper
    {
        /// <summary>
        /// Gets and binds arguments.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <param name="metadata">The metadata to bind to.</param>
        /// <param name="t1">An instance of the in-argument.</param>
        public static void OnGetArguments<T1>(CodeActivityMetadata metadata, InArgument<T1> t1)
        {
            RuntimeArgument t1Argument = new(nameof(T1), typeof(T1), ArgumentDirection.In, true);
            metadata.Bind(t1, t1Argument);

            metadata.SetArgumentsCollection(new Collection<RuntimeArgument>
            {
                t1Argument
            });
        }

        /// <summary>
        /// Gets and binds arguments.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <param name="metadata">The metadata to bind to.</param>
        /// <param name="t1">An instance of the in-argument.</param>
        public static void OnGetArguments<T1>(CodeActivityMetadata metadata, InOutArgument<T1> t1)
        {
            RuntimeArgument t1Argument = new(nameof(T1), typeof(T1), ArgumentDirection.In, true);
            metadata.Bind(t1, t1Argument);

            metadata.SetArgumentsCollection(new Collection<RuntimeArgument> { t1Argument });
        }

        /// <summary>
        /// Gets and binds arguments.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <param name="metadata">The metadata to bind to.</param>
        /// <param name="t1">An instance of the first in-argument.</param>
        /// <param name="t2">An instance of the second in-argument.</param>
        public static void OnGetArguments<T1, T2>(CodeActivityMetadata metadata, InArgument<T1> t1, InArgument<T2> t2)
        {
            RuntimeArgument t1Argument = new(nameof(T1), typeof(T1), ArgumentDirection.In, true);
            RuntimeArgument t2Argument = new(nameof(T2), typeof(T2), ArgumentDirection.In, true);

            metadata.Bind(t1, t1Argument);
            metadata.Bind(t2, t2Argument);

            metadata.SetArgumentsCollection(new Collection<RuntimeArgument>
            {
                t1Argument,
                t2Argument
            });
        }

        /// <summary>
        /// Tries to generate a unary expression.
        /// </summary>
        /// <typeparam name="T1">The input type.</typeparam>
        /// <typeparam name="TResult">The output type, typically the same as <typeparamref name="T1"/>.</typeparam>
        /// <param name="operatorType">The type of operator. Only unary operators are accepted.</param>
        /// <param name="func">The function to generate.</param>
        /// <param name="validationError">If any errors occurred, this value will be null.</param>
        /// <returns><see langword="true"/> when successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGenerateUnaryDelegate<T1, TResult>
        (
            ExpressionType operatorType,
            [NotNullWhen(true)] out Func<T1, TResult>? func,
            [NotNullWhen(false)] out ValidationError? validationError)
        {
            func = null;
            validationError = null;

            ParameterExpression t1Expression = Expression.Parameter(typeof(T1), nameof(T1));

            try
            {
                UnaryExpression binaryExpression = Expression.MakeUnary(operatorType, t1Expression, null);
                Expression<Func<T1, TResult>> lambdaExpression = Expression.Lambda<Func<T1, TResult>>(binaryExpression, t1Expression);
                func = lambdaExpression.Compile();

                return true;
            }
            catch (Exception e)
            {
                validationError = new ValidationError(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Tries to generate a unary expression.
        /// </summary>
        /// <typeparam name="T1">The left operand.</typeparam>
        /// <typeparam name="T2">The right operand.</typeparam>
        /// <typeparam name="TResult">The output type, typically the same as <typeparamref name="T1"/>.</typeparam>
        /// <param name="operatorType">The type of operator. Only unary operators are accepted.</param>
        /// <param name="func">The function to generate.</param>
        /// <param name="validationError">If any errors occurred, this value will be null.</param>
        /// <returns><see langword="true"/> when successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGenerateBinaryDelegate<T1, T2, TResult>
        (
            ExpressionType operatorType,
            [NotNullWhen(true)] out Func<T1, T2, TResult>? func,
            [NotNullWhen(false)] out ValidationError? validationError)
        {
            func = null;
            validationError = null;

            ParameterExpression t1Expression = Expression.Parameter(typeof(T1), nameof(T1));
            ParameterExpression t2Expression = Expression.Parameter(typeof(T2), nameof(T2));

            try
            {
                BinaryExpression binaryExpression = Expression.MakeBinary(operatorType, t1Expression, t2Expression);
                Expression<Func<T1, T2, TResult>> lambdaExpression = Expression.Lambda<Func<T1, T2, TResult>>(binaryExpression, t1Expression, t2Expression);
                func = lambdaExpression.Compile();

                return true;
            }
            catch (Exception e)
            {
                validationError = new ValidationError(e.Message);
                return false;
            }
        }
    }
}
