using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Sales;
using VinylExchange.Models.ResourceModels.Sales;
using VinylExchange.Models.Utility;

namespace VinylExchange.Services.Data.MainServices.Sales
{
    public interface ISalesService
    {
        Task<GetSaleResourceModel> GetSale(Guid saleId);
        Task<Sale> CreateSale(CreateSaleInputModel inputModel,Guid sellerId);
        Task<IEnumerable<GetAllSalesForReleaseResouceModel>> GetAllSalesForRelease(Guid releaseId);
        Task<Sale> PlaceOrder(PlaceOrderInputModel inputModel, Nullable<Guid> buyerId);
        Task<GetSaleInfoUtilityModel> GetSaleInfo(Nullable<Guid> saleId);
        Task<Sale> SetShippingPrice(SetShippingPriceInputModel inputModel);

        Task<IEnumerable<GetUserPurchasesResourceModel>> GetUserPurchases(Guid buyerId);

        Task<IEnumerable<GetUserSalesResourceModel>> GetUserSales(Guid sellerId);

        Task<Sale> CompletePayment(CompletePaymentInputModel inputModel);
    }
}
