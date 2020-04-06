namespace VinylExchange.Services.Data.MainServices.Sales
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Data;
    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Addresses;
    using VinylExchange.Services.Data.MainServices.Releases;
    using VinylExchange.Services.Data.MainServices.Users;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Sales;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    public class SalesService : ISalesService
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IAddressesEntityRetriever addressesEntityRetriever;

        private readonly IUsersEntityRetriever usersEntityRetriever;

        private readonly IReleasesEntityRetriever releasesEntityRetriever;

        public SalesService(
            VinylExchangeDbContext dbContext,
            IAddressesEntityRetriever addressesEntityRetriever,
            IUsersEntityRetriever usersEntityRetriever,
            IReleasesEntityRetriever releasesEntityRetriever)
        {
            this.dbContext = dbContext;
            this.addressesEntityRetriever = addressesEntityRetriever;
            this.usersEntityRetriever = usersEntityRetriever;
            this.releasesEntityRetriever = releasesEntityRetriever;
        }

        public async Task<TModel> CreateSale<TModel>(CreateSaleInputModel inputModel, Guid sellerId)
        {
            var release = await this.releasesEntityRetriever.GetRelease(inputModel.ReleaseId);

            var address = await this.addressesEntityRetriever.GetAddress(inputModel.ShipsFromAddressId);

            var user = await this.usersEntityRetriever.GetUser(sellerId);

            if (release == null)
            {
                throw new NullReferenceException(ReleaseNotFound);
            }

            if (address == null)
            {
                throw new NullReferenceException(AddressNotFound);
            }

            if (user == null)
            {
                throw new NullReferenceException(UserNotFound);
            }

            var sale = inputModel.To<Sale>();

            sale.ShipsFrom = $"{address.Country} - {address.Town}";

            sale.SellerId = sellerId;

            sale.Status = Status.Open;

            var trackedSale = await this.dbContext.Sales.AddAsync(sale);

            await this.dbContext.SaveChangesAsync();

            return trackedSale.Entity.To<TModel>();
        }

        public async Task<TModel> EditSale<TModel>(EditSaleInputModel inputModel)
        {
            var sale = await this.GetSale(inputModel.SaleId);

            var address = await this.addressesEntityRetriever.GetAddress(inputModel.ShipsFromAddressId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

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

        public async Task<TModel> RemoveSale<TModel>(Guid? saleId)
        {
            var sale = await this.GetSale(saleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var removedAddress = this.dbContext.Sales.Remove(sale).Entity;
            await this.dbContext.SaveChangesAsync();

            return removedAddress.To<TModel>();
        }

        public async Task<List<TModel>> GetAllSalesForRelease<TModel>(Guid? releaseId)
        {
            return await this.dbContext.Sales.Where(s => s.ReleaseId == releaseId).Where(s => s.Status == Status.Open)
                       .To<TModel>().ToListAsync();
        }

        public async Task<TModel> GetSale<TModel>(Guid? saleId)
        {
            return await this.dbContext.Sales.Where(s => s.Id == saleId).To<TModel>().FirstOrDefaultAsync();
        }

        public async Task<List<TModel>> GetUserPurchases<TModel>(Guid buyerId)
        {
            return await this.dbContext.Sales.Where(s => s.BuyerId == buyerId).To<TModel>().ToListAsync();
        }

        public async Task<List<TModel>> GetUserSales<TModel>(Guid sellerId)
        {
            return await this.dbContext.Sales.Where(s => s.SellerId == sellerId).To<TModel>().ToListAsync();
        }

        public async Task<TModel> PlaceOrder<TModel>(PlaceOrderInputModel inputModel, Guid? buyerId)
        {
            var sale = await this.GetSale(inputModel.SaleId);

            var address = await this.addressesEntityRetriever.GetAddress(inputModel.AddressId);

            var user = await this.usersEntityRetriever.GetUser(buyerId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            if (address == null)
            {
                throw new NullReferenceException(AddressNotFound);
            }

            if (user == null)
            {
                throw new NullReferenceException(UserNotFound);
            }

            sale.BuyerId = buyerId;
            sale.Status = Status.ShippingNegotiation;
            sale.ShipsTo = $"{address.Country} - {address.Town} - {address.PostalCode} - {address.FullAddress}";

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }

        public async Task<TModel> SetShippingPrice<TModel>(SetShippingPriceInputModel inputModel)
        {
            var sale = await this.GetSale(inputModel.SaleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            sale.ShippingPrice = inputModel.ShippingPrice;
            sale.Status = Status.PaymentPending;

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }

        public async Task<TModel> CompletePayment<TModel>(CompletePaymentInputModel inputModel)
        {
            var sale = await this.GetSale(inputModel.SaleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            sale.Status = Status.Paid;
            sale.OrderId = inputModel.OrderId;

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }

        public async Task<TModel> ConfirmItemSent<TModel>(ConfirmItemSentInputModel inputModel)
        {
            var sale = await this.GetSale(inputModel.SaleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            sale.Status = Status.Sent;

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }

        public async Task<TModel> ConfirmItemRecieved<TModel>(ConfirmItemRecievedInputModel inputModel)
        {
            var sale = await this.GetSale(inputModel.SaleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            sale.Status = Status.Finished;

            await this.dbContext.SaveChangesAsync();

            return sale.To<TModel>();
        }

        private async Task<Sale> GetSale(Guid? saleId)
        {
            return await this.dbContext.Sales.FirstOrDefaultAsync(s => s.Id == saleId);
        }
    }
}