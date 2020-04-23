namespace VinylExchange.Services.Data.MainServices.Sales.Contracts
{
    using System;
    using System.Threading.Tasks;
    using VinylExchange.Data.Models;

    public interface ISalesEntityRetriever
    {
        Task<Sale> GetSale(Guid? saleId);
    }
}