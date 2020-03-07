namespace VinylExchange.Services.Data.HelperServices.Sales
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Web.Models.ResourceModels.SaleMessages;

    public interface ISaleMessagesService
    {
        Task<AddMessageToSaleResourceModel> AddMessageToSale(Guid saleId, Guid userId, string message);

        Task<IEnumerable<GetMessagesForSaleResourceModel>> GetMessagesForSale(Guid saleId);
    }
}