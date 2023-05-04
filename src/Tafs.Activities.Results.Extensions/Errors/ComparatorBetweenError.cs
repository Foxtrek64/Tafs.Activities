//
//  ComparatorBetweenError.cs
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

using Tafs.Activities.Results.Extensions.Errors.Bases;

namespace Tafs.Activities.Results.Extensions.Errors
{
    /// <summary>
    /// A generic error resulting from a value failing to be between two expected values.
    /// </summary>
    /// <typeparam name="TEntity">Some entity type that is equatable.</typeparam>
    /// <param name="Actual">The actual value.</param>
    /// <param name="Minimum">The minimum, exclusive value.</param>
    /// <param name="Maximum">The maximum, inclusive value.</param>
    public sealed record class ComparatorBetweenError<TEntity>(TEntity Actual, TEntity Minimum, TEntity Maximum)
        : ComparatorError<TEntity>(Actual, $"Actual value '{Actual}' must be between [{Minimum}..{Maximum}], upper inclusive.")
#if NET7_0_OR_GREATER
        where TEntity : System.Numerics.IComparisonOperators<TEntity, TEntity, bool>;
#else
        where TEntity : unmanaged;
#endif
}
