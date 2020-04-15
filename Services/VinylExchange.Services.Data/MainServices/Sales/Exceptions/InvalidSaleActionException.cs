using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Services.Data.MainServices.Sales.Exceptions
{
    public class InvalidSaleActionException : Exception
    {
        public  InvalidSaleActionException(string message) : base (message)
        {
        }
    }
}
