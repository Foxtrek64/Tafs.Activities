//
//  ComparisonOperator.cs
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

namespace Tafs.Activities.Results.Extensions
{
    /// <summary>
    /// Enumerates a set of value comparisons.
    /// </summary>
    public enum ComparisonOperator
    {
        /// <summary>
        /// No Comparison should take place. Default.
        /// </summary>
        NoComparison = 0,

        /// <summary>
        /// Value should be between two numbers, upper inclusive.
        /// </summary>
        Between,

        /// <summary>
        /// Value should not be between two numbers.
        /// </summary>
        NotBetween,

        /// <summary>
        /// Value should be equal.
        /// </summary>
        Equal,

        /// <summary>
        /// Value should not be equal.
        /// </summary>
        NotEqual,

        /// <summary>
        /// Value should be greater than.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Value should be less than.
        /// </summary>
        LessThan,

        /// <summary>
        /// Value should be greater than or equal.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Value should be less than or equal.
        /// </summary>
        LessThanOrEqual
    }
}
