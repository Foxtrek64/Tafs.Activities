//
//  ConsoleError.cs
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
    /// Represents an error arising from a console.
    /// </summary>
    /// <param name="ConsoleName">The name of the type of console in which the error occurred, e.g. "batch" or "AS400".</param>
    /// <param name="Message">The error message.</param>
    public abstract record class ConsoleError(string ConsoleName, string Message)
        : ResultError($"Error in {ConsoleName}: {Message}");
}
