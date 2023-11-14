//
//  BusinessError.cs
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

using JetBrains.Annotations;
using Remora.Results;

namespace Tafs.Activities.Results.Extensions.Errors
{
    /// <summary>
    /// Represents a generic error arising from a problem with data or a permanent issue with the environment.
    /// </summary>
    /// <remarks>Intended for persistent issues where simply retrying later will not resolve the issue.</remarks>
    /// <param name="Message">The error message.</param>
    [PublicAPI]
    public sealed record class BusinessError(string Message) : ResultError(Message);
}
