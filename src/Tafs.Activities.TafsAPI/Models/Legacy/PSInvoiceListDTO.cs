//
//  PSInvoiceListDTO.cs
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
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tafs.Activities.TafsAPI.Models.Legacy
{
    public sealed record class PSInvoiceListDTO
    (
        int? TotalRecords,
        long? RowNumber,
        string Id,
        string InvoiceBatchId,
        DateTime? DatetimePosted,
        DateTime? DatetimeDelivery,
        string AccountIdDebtor,
        string DebtorName,
        string DriverId,
        string DriverName,
        string InvoiceNumber,
        string LoadNumber,
        decimal? LinehaulAmount,
        decimal? Amount,
        decimal? PickupLat,
        decimal? PickupLng,
        decimal? DeliveryLat,
        decimal? DeliveryLng,
        string PickupFormattedAddress,
        string DeliveryFormattedAddress,
        string PickupZip,
        string DeliveryZip,
        string PickupState,
        string DeliveryState,
        string DistanceFormatted,
        string DurationFormatted,
        string Status,
        string TimeSpan,
        string StatusColor,
        string EquipmentType,
        string PickupAddress,
        string PickupCity,
        string PickupCounty,
        string PickupCountry,
        string DeliveryAddress,
        string DeliveryCity,
        string DeliveryCounty,
        string DeliveryCountry,
        int? Distance,
        int? Duration,
        decimal? LessAdvancesAmount,
        decimal? DetentionAmount,
        decimal? LumperPaidAmount,
        string PaidByEntity,
        Guid? SecurityUserIdDriver,
        bool IsSelected,
        string Msg,
#pragma warning disable SA1300 // Element should begin with upper-case letter
        string asset_id,
#pragma warning restore SA1300 // Element should begin with upper-case letter
        List<InvoiceTransactionDTO> InvoiceCharges,
        List<InvoiceTransactionDTO> InvoiceDeductions,
        List<InvoiceDocumentDTO> InvoiceDocuments
    )
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PSInvoiceListDTO"/> class.
        /// </summary>
#pragma warning disable SA1611 // Element parameters should be documented
        public PSInvoiceListDTO
        (
            string pickupZip,
            string deliveryZip,
            string id,
            DateTime? dateTimePosted,
            DateTime? dateTimeDelivery,
            string accountIdDebtor,
            string paidByEntity,
            string invoiceNumber,
            string loadNumber,
            decimal? lineHaulAmount,
            decimal? amount,
            string status,
            string equipmentType,
            Guid? securityUserIdDriver,
            bool isSelected,
            string assetId
        )
            : this
        (
            TotalRecords: null,
            RowNumber: null,
            Id: id,
            InvoiceBatchId: string.Empty,
            DatetimePosted: dateTimePosted,
            DatetimeDelivery: dateTimeDelivery,
            AccountIdDebtor: accountIdDebtor,
            DebtorName: string.Empty,
            DriverId: string.Empty,
            DriverName: string.Empty,
            InvoiceNumber: invoiceNumber,
            LoadNumber: loadNumber,
            LinehaulAmount: lineHaulAmount,
            Amount: amount,
            PickupLat: null,
            PickupLng: null,
            DeliveryLat: null,
            DeliveryLng: null,
            PickupFormattedAddress: string.Empty,
            DeliveryFormattedAddress: string.Empty,
            PickupZip: pickupZip,
            DeliveryZip: deliveryZip,
            PickupState: string.Empty,
            DeliveryState: string.Empty,
            DistanceFormatted: string.Empty,
            DurationFormatted: string.Empty,
            Status: status,
            TimeSpan: string.Empty,
            StatusColor: string.Empty,
            EquipmentType: equipmentType,
            PickupAddress: string.Empty,
            PickupCity: string.Empty,
            PickupCounty: string.Empty,
            PickupCountry: string.Empty,
            DeliveryAddress: string.Empty,
            DeliveryCity: string.Empty,
            DeliveryCounty: string.Empty,
            DeliveryCountry: string.Empty,
            Distance: null,
            Duration: null,
            LessAdvancesAmount: null,
            DetentionAmount: null,
            LumperPaidAmount: null,
            PaidByEntity: paidByEntity,
            SecurityUserIdDriver: securityUserIdDriver,
            IsSelected: isSelected,
            Msg: string.Empty,
            asset_id: assetId,
            InvoiceCharges: new(),
            InvoiceDeductions: new(),
            InvoiceDocuments: new()
        )
        {
        }
    }
}
