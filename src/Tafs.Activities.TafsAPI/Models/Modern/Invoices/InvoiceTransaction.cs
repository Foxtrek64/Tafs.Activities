//
//  InvoiceTransaction.cs
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
    /// Defines a transaction item on an invoice.
    /// </summary>
    /// <param name="InvoiceTransactionId">The unique id of this invoice transaction.</param>
    /// <param name="InvoiceId">The unique id of the invoice this transaction belongs to.</param>
    /// <param name="TransactionType">The type of transaction.</param>
    /// <param name="Name">A short description of the transaction.</param>
    /// <param name="Amount">The dollar amount of the transaction.</param>
    public readonly record struct InvoiceTransaction
    (
        string InvoiceTransactionId,
        string InvoiceId,
        string TransactionType,
        string Name,
        decimal Amount
    )
    {
        /// <summary>
        /// Gets the unique id of this invoice transaction.
        /// </summary>
        public readonly string InvoiceTransactionId = InvoiceTransactionId;

        /// <summary>
        /// Gets the unique id of the invoice this transaction belongs to.
        /// </summary>
        public readonly string InvoiceId = InvoiceId;

        /// <summary>
        /// Gets the type of transaction.
        /// </summary>
        public readonly string TransactionType = TransactionType;

        /// <summary>
        /// Gets a short description of the transaction.
        /// </summary>
        public readonly string Name = Name;

        /// <summary>
        /// Gets the dollar amount of the transaction.
        /// </summary>
        public readonly decimal Amount = Amount;
    }
}
