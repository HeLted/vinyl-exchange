using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Sales;
using VinylExchange.Models.ResourceModels.Sales;
using VinylExchange.Models.Utility;
using VinylExchange.Services.Data.MainServices.Addresses;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.Data.MainServices.Sales
{

    public class SalesService : ISalesService
    {
        private readonly VinylExchangeDbContext dbContext;


        public SalesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public async Task<GetSaleResourceModel> GetSale(Guid saleId)
        => await this.dbContext.Sales
                 .Where(s => s.Id == saleId)
                 .To<GetSaleResourceModel>()
                 .FirstOrDefaultAsync();



        public async Task<Sale> CreateSale(CreateSaleInputModel inputModel, Guid sellerId)
        {
            inputModel.SellerId = sellerId;

            var sale = inputModel.To<Sale>();

            var trackedSale = await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            return trackedSale.Entity;

        }

        public async Task<IEnumerable<GetAllSalesForReleaseResouceModel>> GetAllSalesForRelease(Guid releaseId)
            => await this.dbContext.Sales
            .Where(s => s.ReleaseId == releaseId)
            .Where(s => s.Status == Status.Open)
            .To<GetAllSalesForReleaseResouceModel>()
            .ToListAsync();

        public async Task<Sale> PlaceOrder(PlaceOrderInputModel inputModel, Nullable<Guid> buyerId)
        {

            var sale = await this.dbContext.Sales
                .Where(s => s.Id == inputModel.SaleId)
                .FirstOrDefaultAsync();

            var address = await this.dbContext.Addresses
                .Where(a => a.Id == inputModel.AddressId)
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
            var sale = await this.dbContext.Sales
              .Where(s => s.Id == inputModel.SaleId)
              .FirstOrDefaultAsync();

            sale.ShippingPrice = inputModel.ShippingPrice;
            sale.Status = Status.PaymentPending;

            await this.dbContext.SaveChangesAsync();

            return sale;
        }


        public async Task<Sale> CompletePayment(CompletePaymentInputModel inputModel)
        {
            var sale = await this.dbContext.Sales
              .Where(s => s.Id == inputModel.SaleId)
              .FirstOrDefaultAsync();


            sale.Status = Status.Paid;
            sale.OrderId = inputModel.OrderId;

            await this.dbContext.SaveChangesAsync();

            return sale;
        }

        public async Task<IEnumerable<GetUserPurchasesResourceModel>> GetUserPurchases(Guid buyerId)
        {
            var purchases = await this.dbContext.Sales
            .Where(s => s.BuyerId == buyerId)
            .To<GetUserPurchasesResourceModel>()
            .ToListAsync();

            return purchases;
        }


        public async Task<IEnumerable<GetUserSalesResourceModel>> GetUserSales(Guid sellerId)
        {
            var sales = await this.dbContext.Sales
           .Where(s => s.SellerId == sellerId)
           .To<GetUserSalesResourceModel>()
           .ToListAsync();

            return sales;
        }



        public async Task<GetSaleInfoUtilityModel> GetSaleInfo(Nullable<Guid> saleId)
        => await this.dbContext.Sales
            .Where(s => s.Id == saleId)
            .To<GetSaleInfoUtilityModel>()
            .FirstOrDefaultAsync();


    }
}
