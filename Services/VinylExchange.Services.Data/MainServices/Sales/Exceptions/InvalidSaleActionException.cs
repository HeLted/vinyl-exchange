namespace VinylExchange.Services.Data.MainServices.Sales.Exceptions
{
    #region

    using System;

    #endregion

    public class InvalidSaleActionException : Exception
    {
        public InvalidSaleActionException(string message)
            : base(message)
        {
        }
    }
}