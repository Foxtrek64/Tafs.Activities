//
//  InvoiceDocument.cs
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

namespace Tafs.Activities.TafsAPI.Models.Invoices
{
    /// <summary>
    /// Defines the model for an invoice document.
    /// </summary>
    /// <param name="InvoiceDocumentId">The invoice document id.</param>
    /// <param name="InvoiceId">The invoice id.</param>
    /// <param name="DocumentType">The document type.</param>
    /// <param name="ItemSort">This document's place in a sorted collection.</param>
    /// <param name="DecompressDocument">A value indicating whether to decompress the document.</param>
    /// <param name="InvoiceAttachments">A collection of <see cref="InvoiceAttachment"/>s.</param>
    public readonly record struct InvoiceDocument
    (
        string InvoiceDocumentId,
        string InvoiceId,
        string DocumentType,
        int ItemSort,
        bool DecompressDocument,
        InvoiceAttachment[] InvoiceAttachments
    ) : IApiModel
    {
        /// <summary>
        /// Gets the unique id of this <see cref="InvoiceDocument"/>.
        /// </summary>
        public readonly string InvoiceDocumentId = InvoiceDocumentId;

        /// <summary>
        /// Gets the unique id of the invoice this document represents.
        /// </summary>
        public readonly string InvoiceId = InvoiceId;

        /// <summary>
        /// Gets the document type.
        /// </summary>
        public readonly string DocumentType = DocumentType;

        /// <summary>
        /// Gets the 0-based sort ordinal for this document.
        /// </summary>
        public readonly int ItemSort = ItemSort;

        /// <summary>
        /// Gets a value indicating whether the document should be decompressed.
        /// </summary>
        public readonly bool DecompressDocument = DecompressDocument;

        /// <summary>
        /// Gets a collection of invoice attachments.
        /// </summary>
        public readonly InvoiceAttachment[] InvoiceAttachments = InvoiceAttachments;
    }
}
