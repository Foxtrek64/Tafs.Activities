//
//  HttpError.cs
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
using System.Net.Http;
using Remora.Results;

namespace Tafs.Activities.Results.Extensions.Errors
{
    /// <summary>
    /// Represents an error resulting from a web operation.
    /// </summary>
    /// <param name="StatusCode">The <see href="http://www.iana.org/assignments/http-status-codes/http-status-codes.xhtml">HTTP Status Code</see> associated with this error.</param>
    /// <param name="Message">A message which describes the error.</param>
    public sealed record class HttpError(int StatusCode, string? Message = null)
        : ResultError(Message ?? $"An HTTP error occurred ({(ulong)StatusCode} {StatusCode})")
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpError"/> class.
        /// </summary>
        /// <param name="httpResponseMessage">The <see cref="HttpResponseMessage"/> that contains the error.</param>
        public HttpError(HttpResponseMessage httpResponseMessage)
            : this
            (
                  (int)httpResponseMessage.StatusCode,
                  $"Reason: {httpResponseMessage.ReasonPhrase}{Environment.NewLine}{httpResponseMessage.Content.ReadAsStringAsync().Result}"
            )
        {
        }
    }
}
