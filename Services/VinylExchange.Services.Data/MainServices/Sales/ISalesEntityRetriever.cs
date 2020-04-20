namespace VinylExchange.Services.Data.MainServices.Sales
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