//
//  GetInvoices.cs
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
using System.Threading;
using System.Threading.Tasks;
using Tafs.Activities.TafsAPI.Models.Modern;
using Tafs.Activities.TafsAPI.Models.Modern.Invoices;

namespace Tafs.Activities.TafsApi.Activities.Invoices
{
    /// <summary>
    /// Makes a call to api/invoices/{page_number}/{page_size}/{tab}.
    /// </summary>
    public sealed class GetInvoices : AsyncTaskCodeActivity<ApiResult<InvoiceList[]>>
    {
        /// <summary>
        /// Gets or sets the access key used to identify the calling process.
        /// </summary>
        [RequiredArgument]
        public InArgument<string> AccessKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        [DefaultValue(1)]
        [RequiredArgument]
        public InArgument<int> PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of results per page.
        /// </summary>
        [DefaultValue(50)]
        [RequiredArgument]
        public InArgument<int> PageSize { get; set; } = 50;

        /// <summary>
        /// Gets or sets the name of the tab.
        /// </summary>
        [RequiredArgument]
        public InArgument<string> TabName { get; set; } = string.Empty;

        /// <inheritdoc/>
        protected override Task<ApiResult<InvoiceList[]>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
