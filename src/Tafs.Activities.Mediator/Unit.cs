//
//  Unit.cs
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
using System.Threading.Tasks;

namespace Tafs.Activities.Mediator
{
    /// <summary>
    /// Represents a null response.
    /// </summary>
    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
    {
        private static readonly Unit _value = default;

        /// <summary>
        /// Gets an instance of <see cref="Unit"/>.
        /// </summary>
        public static ref readonly Unit Value => ref _value;

        /// <summary>
        /// Gets a <see cref="Unit"/> wrapped in a <see cref="ValueTask{TResult}"/>.
        /// </summary>
        public static ValueTask<Unit> ValueTask => new(_value);

        /// <inheritdoc/>
        public int CompareTo(Unit other) => 0;

        /// <inheritdoc/>
        int IComparable.CompareTo(object obj) => 0;

        /// <inheritdoc/>
        public override int GetHashCode() => 0;

        /// <inheritdoc/>
        public bool Equals(Unit other) => true;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Unit;

        /// <summary>
        /// Indicates whether <paramref name="left"/> is equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Unit"/>.</param>
        /// <param name="right">The second <see cref="Unit"/>.</param>
        public static bool operator ==(Unit left, Unit right) => true;

        /// <summary>
        /// Indicates whether <paramref name="left"/> is not equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Unit"/>.</param>
        /// <param name="right">The second <see cref="Unit"/>.</param>
        public static bool operator !=(Unit left, Unit right) => false;

        /// <inheritdoc/>
        public override string ToString() => "()";
    }
}
