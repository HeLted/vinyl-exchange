namespace VinylExchange.Services.Data.MainServices.Sales.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Data.Common.Enumerations;
    using Web.Models.InputModels.Sales;

    public interface ISalesService
    {
        Task<TModel> CreateSale<TModel>(
            Condition vinylGrade,
            Condition sleeveGrade,
            string description,
            decimal price,
            Guid? shipsFromAddressId,
            Guid? releaseId,
            Guid sellerId);

        Task<TModel> EditSale<TModel>(EditSaleInputModel inputModel);

        Task<TModel> RemoveSale<TModel>(Guid? saleId);

        Task<TModel> GetSale<TModel>(Guid? saleId);

        Task<List<TModel>> GetAllSalesForRelease<TModel>(Guid? releaseId);

        Task<List<TModel>> GetUserPurchases<TModel>(Guid buyerId);

        Task<List<TModel>> GetUserSales<TModel>(Guid sellerId);

        Task<TModel> PlaceOrder<TModel>(Guid? saleId, Guid? addressId, Guid? buyerId);

        Task<TModel> CancelOrder<TModel>(Guid? saleId, Guid? buyerId);

        Task<TModel> SetShippingPrice<TModel>(Guid? saleId, decimal shippingPrice);

        Task<TModel> CompletePayment<TModel>(Guid? saleId, string orderId);

        Task<TModel> ConfirmItemSent<TModel>(Guid? saleId);

        Task<TModel> ConfirmItemRecieved<TModel>(Guid? saleId);
    }
}