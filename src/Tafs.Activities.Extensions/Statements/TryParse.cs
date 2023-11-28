//
//  TryParse.cs
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
using System.ComponentModel;
using System.Globalization;
using Tafs.Activities.Extensions.Extensions;

namespace Tafs.Activities.Extensions.Statements
{
#if NET7_0_OR_GREATER
    /// <summary>
    /// Attempts to parse the specified <typeparamref name="TParsable"/>.
    /// </summary>
    /// <typeparam name="TParsable">The type to parse.</typeparam>
    public sealed class TryParse<TParsable> : CodeActivity<bool>
        where TParsable : IParsable<TParsable>
    {
        private const string EmptyString = "";

        /// <summary>
        /// Gets or sets the value to parse.
        /// </summary>
        [Description("The value to parse.")]
        [RequiredArgument]
        [DefaultValue(EmptyString)]
        public InArgument<string> Value { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the format provider.
        /// </summary>
        [Description("The format provider. If null, uses the invariant culture.")]
        [DefaultValue(null)]
        public InArgument<IFormatProvider?> FormatProvider { get; set; } = new(constValue: null);

        /// <summary>
        /// Gets or sets the parse result.
        /// </summary>
        [Description("The result of the parse.")]
        public OutArgument<TParsable?> ParseResult { get; set; } = new();

        /// <inheritdoc/>
        protected override bool Execute(CodeActivityContext context)
        {
            string value = Value.Get(context);
            var formatProvider = FormatProvider.Get(context, CultureInfo.InvariantCulture);
            var success = TParsable.TryParse(value, formatProvider, out TParsable? result);
            ParseResult.Set(context, result);
            return success;
        }
    }
#endif
}
