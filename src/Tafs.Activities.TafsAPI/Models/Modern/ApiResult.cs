//
//  ApiResult.cs
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

using Remora.Rest.Core;
using Remora.Results;

namespace Tafs.Activities.TafsAPI.Models.Modern
{
    /// <summary>
    /// Defines a wrapper for a response from the TAFS endpoint.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to return.</typeparam>
    /// <param name="APIResponse">A value indicating success, or if an error, the message.</param>
    /// <param name="Data">The data contained in the result.</param>
    public sealed record class ApiResult<TEntity>(Result APIResponse, Optional<TEntity> Data);
}
