//
//  ComparatorError.cs
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

using Remora.Results;

namespace Tafs.Activities.Results.Extensions.Errors.Bases
{
    /// <summary>
    /// Represents an error that occurred when comparing two or more items.
    /// </summary>
    /// <typeparam name="TEntity">The kind of entity being compared.</typeparam>
    /// <param name="Actual">The actual value.</param>
    /// <param name="Message">The reason comparison failed.</param>
    public abstract record class ComparatorError<TEntity>(TEntity Actual, string Message = "Result did not match expected value.")
        : ResultError($"Comparison Error: {Message}");
}
