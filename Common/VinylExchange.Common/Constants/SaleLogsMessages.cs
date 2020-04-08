using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Common.Constants
{
    public class SaleLogsMessages
    {
        public const string PlacedOrder = "Order was placed.Awaiting for seller to specify shipping address.";

        public const string SettedShippingPrice ="Shipping price was set.Awaiting buyer to proceed with payment.";

        public const string Paid = "Payment Confirmed.Awaiting seller to send package.";

        public const string ItemSent = "Item sent out.Awaiting buyer to confirm when package is recieved.";

        public const string ItemRecieved = "Item Recieved.Sale Complete!";

        public const string SaleEdited = "Seller edited sale.";
    }
}
