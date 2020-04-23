namespace VinylExchange.Services.Data.MainServices.Sales.Exceptions
{
    using System;

    public class InvalidSaleActionException : Exception
    {
        public InvalidSaleActionException(string message)
            : base(message)
        {
        }
    }
}