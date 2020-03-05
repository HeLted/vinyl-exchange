using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.ResourceModels.SaleMessages;

namespace VinylExchange.Services.Data.HelperServices.Sales
{
    public interface ISaleMessagesService
    {
        Task<AddMessageToSaleResourceModel> AddMessageToSale(Guid saleId,Guid userId, string message);

        Task<IEnumerable<GetMessagesForSaleResourceModel>> GetMessagesForSale(Guid saleId);
    }
}
