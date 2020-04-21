namespace VinylExchange.Services.Data.MainServices.Sales.Contracts
{
    #region

    using System;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;

    #endregion

    public interface ISalesEntityRetriever
    {
        Task<Sale> GetSale(Guid? saleId);
    }
}