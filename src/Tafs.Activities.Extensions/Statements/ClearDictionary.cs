//
//  ClearDictionary.cs
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
using System.Collections.Generic;
using System.ComponentModel;

namespace Tafs.Activities.Extensions.Statements
{
    /// <summary>
    /// Cleares the provided dictionary.
    /// </summary>
    /// <typeparam name="TKey">The key.</typeparam>
    /// <typeparam name="TValue">The value.</typeparam>
    [Description("Clears the provided dictionary.")]
    public sealed class ClearDictionary<TKey, TValue> : CodeActivity
    {
        /// <summary>
        /// Gets or sets the dictionary to clear.
        /// </summary>
        [RequiredArgument]
        [Description("The dictionary to clear.")]
        public InArgument<IDictionary<TKey, TValue>> Dictionary { get; set; } = new();

        /// <inheritdoc/>
        protected override void Execute(CodeActivityContext context)
        {
            IDictionary<TKey, TValue> dict = Dictionary.Get(context) ?? throw new InvalidOperationException("The provided dictionary is null.");
            dict.Clear();
        }
    }
}
