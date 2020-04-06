namespace VinylExchange.Services.Data.MainServices.Sales
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.InputModels.Sales;

    #endregion

    public interface ISalesService
    {
        Task<TModel> CompletePayment<TModel>(CompletePaymentInputModel inputModel);

        Task<TModel> ConfirmItemRecieved<TModel>(ConfirmItemRecievedInputModel inputModel);

        Task<TModel> ConfirmItemSent<TModel>(ConfirmItemSentInputModel inputModel);

        Task<TModel> CreateSale<TModel>(CreateSaleInputModel inputModel, Guid sellerId);

        Task<TModel> EditSale<TModel>(EditSaleInputModel inputModel);

        Task<List<TModel>> GetAllSalesForRelease<TModel>(Guid? releaseId);

        Task<TModel> GetSale<TModel>(Guid? saleId);

        Task<List<TModel>> GetUserPurchases<TModel>(Guid buyerId);

        Task<List<TModel>> GetUserSales<TModel>(Guid sellerId);

        Task<TModel> PlaceOrder<TModel>(PlaceOrderInputModel inputModel, Guid? buyerId);

        Task<TModel> RemoveSale<TModel>(Guid? saleId);

        Task<TModel> SetShippingPrice<TModel>(SetShippingPriceInputModel inputModel);
    }
}