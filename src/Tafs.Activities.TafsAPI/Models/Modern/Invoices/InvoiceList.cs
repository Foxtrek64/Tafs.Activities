//
//  InvoiceList.cs
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
using Remora.Rest.Core;

namespace Tafs.Activities.TafsAPI.Models.Modern.Invoices
{
    // TODO: Determine the unit of Duration.
    /// <summary>
    /// Represents a response containing a collection of invoices.
    /// </summary>
    /// <param name="TotalRecords">The number of records returned.</param>
    /// <param name="RowNumber">The row number.</param>
    /// <param name="Id">The unique identifer.</param>
    /// <param name="InvoiceBatchId">The unique batch id.</param>
    /// <param name="Posted">The date and time the invoice was posted.</param>
    /// <param name="Pickup">The date and time the order was picked up.</param>
    /// <param name="Delivery">The date and time the order was delivered.</param>
    /// <param name="AccountDebtorId">The unique id of the debtor.</param>
    /// <param name="DebtorName">The debtor's name.</param>
    /// <param name="DriverId">The unique id of the driver.</param>
    /// <param name="DriverName">The driver's name.</param>
    /// <param name="InvoiceNumber">The invoice number.</param>
    /// <param name="LoadNumber">The load number.</param>
    /// <param name="LinehaulAmount">The linehaul amount.</param>
    /// <param name="Amount">The total invoice amount.</param>
    /// <param name="PickupLatitude">The latitude of the pickup location.</param>
    /// <param name="PickupLongitude">The longitude of the pickup location.</param>
    /// <param name="DeliveryLatitude">The latitude of the drop-off location.</param>
    /// <param name="DeliveryLongitude">The longitude of the drop-off location.</param>
    /// <param name="PickupFormattedAddress">The formatted pickup address.</param>
    /// <param name="DeliveryFormattedAddress">The formatted drop-off address.</param>
    /// <param name="PickupZip">The zip code of the pickup location.</param>
    /// <param name="DeliveryZip">The zip code of the drop-off location.</param>
    /// <param name="PickupState">The state of the pickup location.</param>
    /// <param name="DeliveryState">The state of the drop-off location.</param>
    /// <param name="DistanceFormatted">The distance, formatted as a string.</param>
    /// <param name="DurationFormatted">The duration, formatted as a string.</param>
    /// <param name="Status">The status of the invoice.</param>
    /// <param name="TimeSpan">The duration as a timespan.</param>
    /// <param name="StatusColor">The status color.</param>
    /// <param name="EquipmentType">The type of equipment used to move the load.</param>
    /// <param name="PickupAddress">The street address of the pickup location.</param>
    /// <param name="PickupCity">The city of the pickup location.</param>
    /// <param name="PickupCounty">The county of the pickup location.</param>
    /// <param name="PickupCountry">The country of the pickup location.</param>
    /// <param name="DeliveryAddress">The street address of the delivery location.</param>
    /// <param name="DeliveryCity">The city of the delivary location.</param>
    /// <param name="DeliveryCounty">The county of the delivery location.</param>
    /// <param name="DeliveryCountry">The country of the delivery location.</param>
    /// <param name="Distance">The distance in miles.</param>
    /// <param name="Duration">The duration in an unknown unit.</param>
    /// <param name="LessAdvancesAmount">The <paramref name="Amount"/>, minus any advances.</param>
    /// <param name="DetentionAmount">The amount in detention.</param>
    /// <param name="LumperPaidAmount">The amount paid in the lumper.</param>
    /// <param name="PaidByEntity">Who the invoice was paid by.</param>
    /// <param name="SecurityUserIdDriver">The security id of the driver.</param>
    /// <param name="IsCharge">Some unknown flag.</param>
    /// <param name="IsSelected">Some other unknown flag.</param>
    /// <param name="Message">A message tied to the invoice.</param>
    /// <param name="IsPaymentUnapplied">A value indicating whether the payment has completed.</param>
    /// <param name="IsInDispute">A value indicating whether the payment is disputed.</param>
    /// <param name="DisputeReason">If <paramref name="IsInDispute"/> is <see langword="true"/>, a reason why the invoice is disputed.</param>
    /// <param name="Balance">The remaining unpaid balance of the invoice.</param>
    /// <param name="IsNonFunded">A value indicating whether the invoice has been funded.</param>
    /// <param name="AssetId">The asset id.</param>
    /// <param name="AssetName">The asset name.</param>
    /// <param name="InvoiceCharges">A collection of charge transactions on the invoice.</param>
    /// <param name="InvoiceDeductions">A collection of deduction transactions on the invoice.</param>
    /// <param name="InvoiceDocuments">A collection of affiliated invoice documents.</param>
    public readonly record struct InvoiceList
    (
        Optional<int> TotalRecords,
        Optional<int> RowNumber,
        Optional<string> Id,
        Optional<string> InvoiceBatchId,
        [property: JsonProperty("DatetimePosted")] Optional<DateTimeOffset> Posted,
        [property: JsonProperty("DatetimePickup")] Optional<DateTimeOffset> Pickup,
        [property: JsonProperty("DatetimeDelivery")] Optional<DateTimeOffset> Delivery,
        Optional<string> AccountDebtorId,
        Optional<string> DebtorName,
        Optional<string> DriverId,
        Optional<string> DriverName,
        Optional<string> InvoiceNumber,
        Optional<string> LoadNumber,
        Optional<decimal> LinehaulAmount,
        Optional<decimal> Amount,
        [property: JsonProperty("PickupLat")] Optional<double> PickupLatitude,
        [property: JsonProperty("PickupLng")] Optional<double> PickupLongitude,
        [property: JsonProperty("DeliveryLat")] Optional<double> DeliveryLatitude,
        [property: JsonProperty("DeliveryLng")] Optional<double> DeliveryLongitude,
        Optional<string> PickupFormattedAddress,
        Optional<string> DeliveryFormattedAddress,
        Optional<string> PickupZip,
        Optional<string> DeliveryZip,
        Optional<string> PickupState,
        Optional<string> DeliveryState,
        Optional<string> DistanceFormatted,
        Optional<string> DurationFormatted,
        Optional<string> Status,
        Optional<TimeSpan> TimeSpan,
        Optional<string> StatusColor,
        Optional<string> EquipmentType,
        Optional<string> PickupAddress,
        Optional<string> PickupCity,
        Optional<string> PickupCounty,
        Optional<string> PickupCountry,
        Optional<string> DeliveryAddress,
        Optional<string> DeliveryCity,
        Optional<string> DeliveryCounty,
        Optional<string> DeliveryCountry,
        Optional<int> Distance,
        Optional<int> Duration,
        Optional<decimal> LessAdvancesAmount,
        Optional<decimal> DetentionAmount,
        Optional<decimal> LumperPaidAmount,
        Optional<string> PaidByEntity,
        Optional<string> SecurityUserIdDriver,
        [property: JsonProperty("charge_flag")] Optional<bool> IsCharge,
        Optional<bool> IsSelected,
        [property: JsonProperty("Msg")] Optional<string> Message,
        [property: JsonProperty("unapplied_payment_flag")] Optional<bool> IsPaymentUnapplied,
        [property: JsonProperty("in_dispute")] Optional<bool> IsInDispute,
        [property: JsonProperty("in_dispute_reason")] Optional<string> DisputeReason,
        [property: JsonProperty("balance")] Optional<decimal> Balance,
        [property: JsonProperty("non_funded_flag")] Optional<bool> IsNonFunded,
        [property: JsonProperty("asset_id")] Optional<string> AssetId,
        [property: JsonProperty("asset_name")] Optional<string> AssetName,
        InvoiceTransaction[] InvoiceCharges,
        InvoiceTransaction[] InvoiceDeductions,
        InvoiceDocument[] InvoiceDocuments
    );
}
