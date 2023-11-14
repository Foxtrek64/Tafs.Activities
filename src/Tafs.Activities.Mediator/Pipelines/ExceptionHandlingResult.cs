//
//  ExceptionHandlingResult.cs
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

using System.Threading.Tasks;

namespace Tafs.Activities.Mediator.Pipelines
{
    /// <summary>
    /// Represents the result of handling an exception in a pipeline.
    /// </summary>
    /// <typeparam name="TResponse">The type of response.</typeparam>
    public readonly struct ExceptionHandlingResult<TResponse>
    {
        /// <summary>
        /// The response.
        /// </summary>
        internal readonly TResponse _response;

        /// <summary>
        /// A value indicating whether the exception was handled.
        /// </summary>
        internal readonly bool _handled;

        /// <summary>
        /// Gets a new default instance where the exception has not been handled.
        /// </summary>
        public static readonly ExceptionHandlingResult<TResponse> NotHandled = new(false, default!);

        /// <summary>
        /// Gets a new default instance where the exception has been handled.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>An <see cref="ExceptionHandlingResult{TResponse}"/> with the response bundled within.</returns>
        public static ExceptionHandlingResult<TResponse> Handled(TResponse response) => new(true, response);

        private ExceptionHandlingResult(bool handled, TResponse response)
        {
            _handled = handled;
            _response = response;
        }

        /// <summary>
        /// Implicilty converts an <see cref="ExceptionHandlingResult{TResponse}"/> into a <see cref="ValueTask{TResult}"/> containing the result.
        /// </summary>
        /// <param name="result">The <see cref="ExceptionHandlingResult{TResponse}"/> to wrap in the <see cref="ValueTask{TResult}"/>.</param>
        public static implicit operator ValueTask<ExceptionHandlingResult<TResponse>>(ExceptionHandlingResult<TResponse> result) => new(result);
    }
}
