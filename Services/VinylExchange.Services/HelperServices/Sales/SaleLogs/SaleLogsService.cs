namespace VinylExchange.Services.Data.HelperServices.Sales.SaleLogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.SaleLogs;

    public class SaleLogsService : ISaleLogsService
    {
        private readonly VinylExchangeDbContext dbContext;

        public SaleLogsService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AddLogToSaleResourceModel> AddLogToSale(Guid saleId, SaleLogs logType)
        {
            bool isSaleExists = this.dbContext.Sales.Where(s => s.Id == saleId).FirstOrDefault() != null;

            if (!isSaleExists)
            {
                throw new NullReferenceException("Sale with this Id doesn't exist!");
            }

            string logMessage = null;

            switch (logType)
            {
                case SaleLogs.PlacedOrder:
                    logMessage = "Order was placed.Awaiting for seller to specify shipping address.";
                    break;
                case SaleLogs.SettedShippingPrice:
                    logMessage = "Shipping price was set.Awaiting buyer to proceed with payment.";
                    break;
                case SaleLogs.Paid:
                    logMessage = "Payment Confirmed.Awaiting seller to send package.";
                    break;
                case SaleLogs.ItemSent:
                    logMessage = "Item sent out.Awaiting buyer to confirm when package is recieved.";
                    break;
                case SaleLogs.ItemRecieved:
                    logMessage = "Item Recieved.Sale Complete!";
                    break;
            }

            AddLogToSaleResourceModel saleLog =
                (await this.dbContext.SaleLogs.AddAsync(new SaleLog() { Content = logMessage, SaleId = saleId })).Entity
                .To<AddLogToSaleResourceModel>();

            await this.dbContext.SaveChangesAsync();

            return saleLog;
        }

        public async Task<IEnumerable<GetLogsForSaleResourceModel>> GetLogsForSale(Guid saleId) =>
            await this.dbContext.SaleLogs.Where(sl => sl.SaleId == saleId).OrderBy(sl => sl.CreatedOn)
                .To<GetLogsForSaleResourceModel>().ToListAsync();
    }
}