namespace VinylExchange.Services.Data.MainServices.Sales
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using VinylExchange.Data;
    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.HelperServices.Releases;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Sales;
    using VinylExchange.Web.Models.ResourceModels.Sales;
    using VinylExchange.Web.Models.Utility;

    public class SalesService : ISalesService
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IReleaseFilesService releaseFileService;

        public SalesService(VinylExchangeDbContext dbContext, IReleaseFilesService releaseFileService)
        {
            this.dbContext = dbContext;
            this.releaseFileService = releaseFileService;
        }

        public async Task<Sale> CompletePayment(CompletePaymentInputModel inputModel)
        {
            Sale sale = await this.dbContext.Sales.Where(s => s.Id == inputModel.SaleId).FirstOrDefaultAsync();

            sale.Status = Status.Paid;
            sale.OrderId = inputModel.OrderId;

            await this.dbContext.SaveChangesAsync();

            return sale;
        }

        public async Task<Sale> CreateSale(CreateSaleInputModel inputModel, Guid sellerId)
        {
            Sale sale = inputModel.To<Sale>();

            sale.SellerId = sellerId;

            sale.Status = Status.Open;

            EntityEntry<Sale> trackedSale = await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            return trackedSale.Entity;
        }

        public async Task<IEnumerable<GetAllSalesForReleaseResouceModel>> GetAllSalesForRelease(Guid releaseId)
        {
            return await this.dbContext.Sales.Where(s => s.ReleaseId == releaseId).Where(s => s.Status == Status.Open)
                       .To<GetAllSalesForReleaseResouceModel>().ToListAsync();
        }

        public async Task<GetSaleResourceModel> GetSale(Guid saleId)
        {
            return await this.dbContext.Sales.Where(s => s.Id == saleId).To<GetSaleResourceModel>()
                       .FirstOrDefaultAsync();
        }

        public async Task<GetSaleInfoUtilityModel> GetSaleInfo(Guid? saleId)
        {
            return await this.dbContext.Sales.Where(s => s.Id == saleId).To<GetSaleInfoUtilityModel>()
                       .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetUserPurchasesResourceModel>> GetUserPurchases(Guid buyerId)
        {
            List<GetUserPurchasesResourceModel> purchases = await this.dbContext.Sales
                                                                .Where(s => s.BuyerId == buyerId)
                                                                .To<GetUserPurchasesResourceModel>().ToListAsync();

            purchases.ForEach(
                s =>
                    {
                        s.CoverArt = this.releaseFileService.GetReleaseCoverArt(s.ReleaseId).GetAwaiter().GetResult();
                    });

            return purchases;
        }

        public async Task<IEnumerable<GetUserSalesResourceModel>> GetUserSales(Guid sellerId)
        {
            List<GetUserSalesResourceModel> sales = await this.dbContext.Sales.Where(s => s.SellerId == sellerId)
                                                        .To<GetUserSalesResourceModel>().ToListAsync();

            sales.ForEach(
                s =>
                    {
                        s.CoverArt = this.releaseFileService.GetReleaseCoverArt(s.ReleaseId).GetAwaiter().GetResult();
                    });

            return sales;
        }

        public async Task<Sale> PlaceOrder(PlaceOrderInputModel inputModel, Guid? buyerId)
        {
            Sale sale = await this.dbContext.Sales.Where(s => s.Id == inputModel.SaleId).FirstOrDefaultAsync();

            Address address = await this.dbContext.Addresses.Where(a => a.Id == inputModel.AddressId)
                                  .FirstOrDefaultAsync();

            if (address == null)
            {
                throw new NullReferenceException("Address with this Id doesn't exist!");
            }

            sale.BuyerId = buyerId;
            sale.Status = Status.ShippingNegotiation;
            sale.ShipsTo = $"{address.Country} - {address.Town} - {address.PostalCode} - {address.FullAddress}";

            await this.dbContext.SaveChangesAsync();

            return sale;
        }

        public async Task<Sale> SetShippingPrice(SetShippingPriceInputModel inputModel)
        {
            Sale sale = await this.dbContext.Sales.Where(s => s.Id == inputModel.SaleId).FirstOrDefaultAsync();

            sale.ShippingPrice = inputModel.ShippingPrice;
            sale.Status = Status.PaymentPending;

            await this.dbContext.SaveChangesAsync();

            return sale;
        }
    }
}