//
//  BatchUploadError.cs
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

namespace Tafs.Activities.Results.Extensions.Errors
{
    /// <summary>
    /// Represents an error resulting from a partial upload of a set of files.
    /// </summary>
    /// <param name="DocumentsUploaded">The number of documents that were properly uploaded.</param>
    /// <param name="Message">The error message.</param>
    /// <param name="ExpectedDocumentsCount">The number of documents that should have been uploaded.</param>
    public sealed record class BatchUploadError
    (
        int DocumentsUploaded,
        string Message,
        int? ExpectedDocumentsCount = null
    ) : ResultError($"Uploaded {DocumentsUploaded}{(ExpectedDocumentsCount is not null ? $"/{ExpectedDocumentsCount}" : string.Empty)} documents: {Message}");
}
