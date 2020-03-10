namespace VinylExchange.Services.Data.MainServices.Sales
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;
    using VinylExchange.Web.Models.InputModels.Sales;
    using VinylExchange.Web.Models.ResourceModels.Sales;
    using VinylExchange.Web.Models.Utility;

    public interface ISalesService
    {
        Task<Sale> CompletePayment(CompletePaymentInputModel inputModel);

        Task<Sale> ConfirmItemRecieved(ConfirmItemRecievedInputModel inputModel);

        Task<Sale> ConfirmItemSent(ConfirmItemSentInputModel inputModel);

        Task<Sale> CreateSale(CreateSaleInputModel inputModel, Guid sellerId);

        Task<IEnumerable<GetAllSalesForReleaseResouceModel>> GetAllSalesForRelease(Guid releaseId);

        Task<GetSaleResourceModel> GetSale(Guid saleId);

        Task<GetSaleInfoUtilityModel> GetSaleInfo(Guid? saleId);

        Task<IEnumerable<GetUserPurchasesResourceModel>> GetUserPurchases(Guid buyerId);

        Task<IEnumerable<GetUserSalesResourceModel>> GetUserSales(Guid sellerId);

        Task<Sale> PlaceOrder(PlaceOrderInputModel inputModel, Guid? buyerId);

        Task<Sale> SetShippingPrice(SetShippingPriceInputModel inputModel);
    }
}