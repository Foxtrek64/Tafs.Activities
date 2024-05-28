//
//  InvoiceAttachment.cs
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
using Newtonsoft.Json;

namespace Tafs.Activities.TafsAPI.Models.Invoices
{
    /// <summary>
    /// Defines an attachment for the invoice.
    /// </summary>
    /// <param name="InvoiceAttachmentId">The unique id of this invoice attachment.</param>
    /// <param name="InvoiceDocumentId">The id of the document this attachment belongs to.</param>
    /// <param name="FileName">The file name.</param>
    /// <param name="Extension">The file extension.</param>
    /// <param name="Size">The size in bytes.</param>
    /// <param name="Created">The date and time the file was created.</param>
    /// <param name="Attachment">The attachment.</param>
    /// <param name="TiffFilePath">The file path to the tiff image.</param>
    /// <param name="SecurityUserId">The unique id of the user who created the invoice.</param>
    public readonly record struct InvoiceAttachment
    (
        string InvoiceAttachmentId,
        string InvoiceDocumentId,
        string FileName,
        string Extension,
        int Size,
        DateTimeOffset Created,
        string Attachment,
        string TiffFilePath,
        string SecurityUserId
    ) : IApiModel
    {
        /// <summary>
        /// Gets the unique id for this <see cref="InvoiceAttachment"/>.
        /// </summary>
        public readonly string InvoiceAttachmentId = InvoiceAttachmentId;

        /// <summary>
        /// Gets the unique id for the <see cref="InvoiceDocument"/> this <see cref="InvoiceAttachment"/> belongs to.
        /// </summary>
        public readonly string InvoiceDocumentId = InvoiceDocumentId;

        /// <summary>
        /// Gets the file name for this attachment, without the extension.
        /// </summary>
        public readonly string FileName = FileName;

        /// <summary>
        /// Gets the extension for this attachment.
        /// </summary>
        public readonly string Extension = Extension;

        /// <summary>
        /// Gets a size of the attachment in bytes.
        /// </summary>
        public readonly int Size = Size;

        /// <summary>
        /// Gets the date and time this file was created.
        /// </summary>
        [JsonProperty("DatetimeCreated")]
        public readonly DateTimeOffset Created = Created;

        /// <summary>
        /// Gets the contents of the attachment as a string.
        /// </summary>
        public readonly string Attachment = Attachment;

        /// <summary>
        /// Gets the file path to the associated TIFF file.
        /// </summary>
        [JsonProperty("TifFilePath")]
        public readonly string TiffFilePath = TiffFilePath;

        /// <summary>
        /// Gets the unique id of the user who uploaded this attachment.
        /// </summary>
        public readonly string SecurityUserId = SecurityUserId;
    }
}
