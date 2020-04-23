namespace VinylExchange.Services.Data.HelperServices.Sales.SaleMessages
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISaleMessagesService
    {
        Task<TModel> AddMessageToSale<TModel>(Guid? saleId, Guid? userId, string message);

        Task<int> ClearSaleMessages(Guid? saleId);

        Task<IEnumerable<TModel>> GetMessagesForSale<TModel>(Guid? saleId);
    }
}