namespace VinylExchange.Services.Data.HelperServices.Sales.SaleLogs
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Sales.Contracts;
    using VinylExchange.Services.Mapping;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;
    using static VinylExchange.Common.Constants.SaleLogsMessages;

    #endregion

    public class SaleLogsService : ISaleLogsService
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ISalesEntityRetriever salesEntityRetriever;

        public SaleLogsService(VinylExchangeDbContext dbContext, ISalesEntityRetriever salesEntityRetriever)
        {
            this.dbContext = dbContext;
            this.salesEntityRetriever = salesEntityRetriever;
        }

        public async Task<TModel> AddLogToSale<TModel>(Guid? saleId, SaleLogs logType)
        {
            var sale = this.dbContext.Sales.Where(s => s.Id == saleId).FirstOrDefault();

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var logMessage = this.GenerateLogMessage(logType);

            var saleLog =
                (await this.dbContext.SaleLogs.AddAsync(new SaleLog { Content = logMessage, SaleId = saleId })).Entity
                .To<TModel>();

            await this.dbContext.SaveChangesAsync();

            return saleLog;
        }

        public async Task<int> ClearSaleLogs(Guid? saleId)
        {
            var sale = await this.salesEntityRetriever.GetSale(saleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var saleLogs = this.dbContext.SaleLogs.Where(sl => sl.SaleId == sale.Id).ToList();

            var logsToBeClearedNumber = saleLogs.Count;

            this.dbContext.SaleLogs.RemoveRange(saleLogs);

            await this.dbContext.SaveChangesAsync();

            return logsToBeClearedNumber;
        }

        public async Task<IEnumerable<TModel>> GetLogsForSale<TModel>(Guid? saleId)
        {
            return await this.dbContext.SaleLogs.Where(sl => sl.SaleId == saleId).OrderBy(sl => sl.CreatedOn)
                       .To<TModel>().ToListAsync();
        }

        private string GenerateLogMessage(SaleLogs logType)
        {
            string logMessage = null;

            switch (logType)
            {
                case SaleLogs.PlacedOrder:
                    logMessage = PlacedOrder;
                    break;
                case SaleLogs.SettedShippingPrice:
                    logMessage = SettedShippingPrice;
                    break;
                case SaleLogs.Paid:
                    logMessage = Paid;
                    break;
                case SaleLogs.ItemSent:
                    logMessage = ItemSent;
                    break;
                case SaleLogs.ItemRecieved:
                    logMessage = ItemRecieved;
                    break;
                case SaleLogs.SaleEdited:
                    logMessage = SaleEdited;
                    break;
            }

            return logMessage;
        }
    }
}