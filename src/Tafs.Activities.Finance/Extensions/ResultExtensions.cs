//
//  ResultExtensions.cs
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
using System.Collections.Generic;
using System.Text;
using Remora.Results;
using Tafs.Activities.Results.Extensions.Errors;
using Vogen;

namespace Tafs.Activities.Finance.Extensions
{
    /// <summary>
    /// Contains a set of extensions for <see cref="Result"/> and <see cref="Result{TEntity}"/>.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Converts the <see cref="ValueObjectOrError{T}"/> into an instance of <see cref="Result{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The underlying entity.</typeparam>
        /// <param name="validationResult">The result.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the underlying entity or a <see cref="ValidationError"/>.</returns>
        public static Result<TEntity> AsResult<TEntity>(this ValueObjectOrError<TEntity> validationResult)
        {
            return validationResult.IsSuccess
                ? validationResult.ValueObject
                : new ValidationError(validationResult.Error.ErrorMessage);
        }

        /// <summary>
        /// Converts the <see cref="ValueObjectOrError{T}"/> into an instance of <see cref="Result{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TCurrent">The entity contained by <paramref name="validationResult"/>.</typeparam>
        /// <typeparam name="TNew">The new type.</typeparam>
        /// <param name="validationResult">The result.</param>
        /// <param name="converter">A conversion function to transform the <typeparamref name="TCurrent"/> instance into an instance of <typeparamref name="TNew"/>.</param>
        /// <returns>A <see cref="Result{TEntity}"/> containing the underlying entity or a <see cref="ValidationError"/>.</returns>
        public static Result<TNew> AsResult<TCurrent, TNew>(this ValueObjectOrError<TCurrent> validationResult, Func<TCurrent, TNew> converter)
        {
            return validationResult.IsSuccess
                ? converter(validationResult.ValueObject)
                : new ValidationError(validationResult.Error.ErrorMessage);
        }
    }
}
