namespace VinylExchange.Services.Data.MainServices.Sales
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VinylExchange.Data;
    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.HelperServices.Releases;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Sales;
    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    public class SalesService : ISalesService
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IReleaseFilesService releaseFileService;

        public SalesService(VinylExchangeDbContext dbContext, IReleaseFilesService releaseFileService)
        {
            this.dbContext = dbContext;
            this.releaseFileService = releaseFileService;
        }

        public async Task<TModel> CompletePayment<TModel>(CompletePaymentInputModel inputModel)
        {
            Sale sale = await this.GetSale(inputModel.SaleId);

            sale.Status = Status.Paid;
            sale.OrderId = inputModel.OrderId;

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }

        public async Task<TModel> ConfirmItemRecieved<TModel>(ConfirmItemRecievedInputModel inputModel)
        {
            Sale sale = await this.GetSale(inputModel.SaleId);

            sale.Status = Status.Finished;

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }

        public async Task<TModel> ConfirmItemSent<TModel>(ConfirmItemSentInputModel inputModel)
        {
            Sale sale = await this.GetSale(inputModel.SaleId);

            sale.Status = Status.Sent;

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }

        public async Task<TModel> CreateSale<TModel>(CreateSaleInputModel inputModel, Guid sellerId)
        {
            Sale sale = inputModel.To<Sale>();

            var address = await this.GetAddress(inputModel.ShipsFromAddressId);

            if (address == null)
            {
                throw new NullReferenceException(AddressNotFound);
            }

            sale.ShipsFrom = $"{address.Country} - {address.Town}";

            sale.SellerId = sellerId;

            sale.Status = Status.Open;

            EntityEntry<Sale> trackedSale = await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            return trackedSale.Entity.To<TModel>();
        }

        public async Task<TModel> EditSale<TModel>(EditSaleInputModel inputModel)
        {

            Sale sale = await this.GetSale(inputModel.SaleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }


            var address = await this.dbContext.Addresses.Where(a => a.Id == inputModel.ShipsFromAddressId)
                              .FirstOrDefaultAsync();

            if (address == null)
            {
                throw new NullReferenceException(AddressNotFound);
            }


            sale.Price = inputModel.Price;

            sale.SleeveGrade = inputModel.SleeveGrade;

            sale.VinylGrade = inputModel.VinylGrade;

            sale.Description = inputModel.Description;

            sale.ShipsFrom = $"{address.Country} - {address.Town}";

            sale.Status = Status.Open;

            sale.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }


        public async Task<List<TModel>> GetAllSalesForRelease<TModel>(Guid releaseId)
        {
            return await this.dbContext.Sales.Where(s => s.ReleaseId == releaseId).Where(s => s.Status == Status.Open)
                       .To<TModel>().ToListAsync();
        }

        public async Task<TModel> GetSale<TModel>(Guid saleId)
        {
            return await this.dbContext.Sales.Where(s => s.Id == saleId).To<TModel>()
                       .FirstOrDefaultAsync();
        }

        public async Task<TModel> GetSaleInfo<TModel>(Guid? saleId)
        {
            return await this.dbContext.Sales.Where(s => s.Id == saleId).To<TModel>()
                       .FirstOrDefaultAsync();
        }

        public async Task<List<TModel>> GetUserPurchases<TModel>(Guid buyerId)
        {
            List<TModel> purchases = await this.dbContext.Sales
                                                                .Where(s => s.BuyerId == buyerId)
                                                                .To<TModel>().ToListAsync();

            purchases.ForEach(
                s =>
                    {
                        var coverArtProperty = s.GetType().GetProperty("CoverArt");
                        var releaseIdProperty = (Guid)s.GetType().GetProperty("ReleaseId").GetValue(s);

                        coverArtProperty.SetValue(s, this.releaseFileService.GetReleaseCoverArt(releaseIdProperty).GetAwaiter().GetResult());
                    });

            return purchases;
        }

        public async Task<List<TModel>> GetUserSales<TModel>(Guid sellerId)
        {
            List<TModel> sales = await this.dbContext.Sales.Where(s => s.SellerId == sellerId)
                                                        .To<TModel>().ToListAsync();

            sales.ForEach(
                s =>
                    {
                        var coverArtProperty = s.GetType().GetProperty("CoverArt");
                        var releaseIdProperty = (Guid)s.GetType().GetProperty("ReleaseId").GetValue(s);

                        coverArtProperty.SetValue(s, this.releaseFileService.GetReleaseCoverArt(releaseIdProperty).GetAwaiter().GetResult());
                    });

            return sales;
        }

        public async Task<TModel> PlaceOrder<TModel>(PlaceOrderInputModel inputModel, Guid? buyerId)
        {
            Sale sale = await this.GetSale(inputModel.SaleId);

            var address = await this.GetAddress(inputModel.AddressId);

            if (address == null)
            {
                throw new NullReferenceException(AddressNotFound);
            }

            sale.BuyerId = buyerId;
            sale.Status = Status.ShippingNegotiation;
            sale.ShipsTo = $"{address.Country} - {address.Town} - {address.PostalCode} - {address.FullAddress}";

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }

        public async Task<TModel> SetShippingPrice<TModel>(SetShippingPriceInputModel inputModel)
        {
            Sale sale = await this.GetSale(inputModel.SaleId);

            sale.ShippingPrice = inputModel.ShippingPrice;
            sale.Status = Status.PaymentPending;

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }

        public async Task<TModel> RemoveSale<TModel>(Guid saleId)
        {
            Sale sale = await this.GetSale(saleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var removedAddress = this.dbContext.Sales.Remove(sale).Entity;
            await this.dbContext.SaveChangesAsync();

            return removedAddress.To<TModel>();
        }

        private async Task<Sale> GetSale(Guid? saleId) => 
            await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == saleId);

        private async Task<Address> GetAddress(Guid? addressId) =>
        await this.dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
    }
}