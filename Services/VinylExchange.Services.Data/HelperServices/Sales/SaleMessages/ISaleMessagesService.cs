namespace VinylExchange.Services.Data.HelperServices.Sales.SaleMessages
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.ResourceModels.SaleMessages;

    #endregion

    public interface ISaleMessagesService
    {
        Task<AddMessageToSaleResourceModel> AddMessageToSale(Guid saleId, Guid userId, string message);

        Task<IEnumerable<GetMessagesForSaleResourceModel>> GetMessagesForSale(Guid saleId);

        Task<int> ClearSaleMessages(Guid? saleId);
    }
}