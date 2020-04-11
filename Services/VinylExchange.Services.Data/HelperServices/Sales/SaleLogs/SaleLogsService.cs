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
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.SaleLogs;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;
    using static VinylExchange.Common.Constants.SaleLogsMessages;

    #endregion

    public class SaleLogsService : ISaleLogsService
    {
        private readonly VinylExchangeDbContext dbContext;

        public SaleLogsService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TModel> AddLogToSale<TModel>(Guid? saleId, SaleLogs logType)
        {
            var sale = this.dbContext.Sales.Where(s => s.Id == saleId).FirstOrDefault();

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            string logMessage = this.GenerateLogMessage(logType);

            var saleLog =
                (await this.dbContext.SaleLogs.AddAsync(new SaleLog { Content = logMessage, SaleId = saleId })).Entity
                .To<TModel>();

            await this.dbContext.SaveChangesAsync();

            return saleLog;
        }

        public async Task<int> ClearSaleLogs(Guid? saleId)
        {
            var sale = this.dbContext.Sales.Where(s => s.Id == saleId).FirstOrDefault();

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var logsToBeClearedNumber = sale.Logs.Count;

            sale.Logs.Clear();

            await this.dbContext.SaveChangesAsync();

            return logsToBeClearedNumber;
        }

        public async Task<IEnumerable<GetLogsForSaleResourceModel>> GetLogsForSale(Guid saleId)
        {
            return await this.dbContext.SaleLogs.Where(sl => sl.SaleId == saleId).OrderBy(sl => sl.CreatedOn)
                       .To<GetLogsForSaleResourceModel>().ToListAsync();
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