using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Data.Common.Enumerations
{
    public enum Status
    {
        Open = 1,
        ShippingNegotiation=2,
        PaymentPending=3,
        Paid = 4,
        Sent= 5,
        Finished=6,
        closed=7
        
    }
}
