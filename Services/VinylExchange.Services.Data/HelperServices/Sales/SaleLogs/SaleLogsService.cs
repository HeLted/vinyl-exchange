namespace VinylExchange.Services.Data.HelperServices.Sales.SaleLogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using MainServices.Sales.Contracts;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using static Common.Constants.NullReferenceExceptionsConstants;


    public class SaleLogsService : ISaleLogsService
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly ISalesEntityRetriever salesEntityRetriever;

        public SaleLogsService(VinylExchangeDbContext dbContext, ISalesEntityRetriever salesEntityRetriever)
        {
            this.dbContext = dbContext;
            this.salesEntityRetriever = salesEntityRetriever;
        }

        public async Task<TModel> AddLogToSale<TModel>(Guid? saleId, Common.Enumerations.SaleLogs logType)
        {
            var sale = dbContext.Sales.Where(s => s.Id == saleId).FirstOrDefault();

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var logMessage = GenerateLogMessage(logType);

            var saleLog =
                (await dbContext.SaleLogs.AddAsync(new SaleLog {Content = logMessage, SaleId = saleId})).Entity
                .To<TModel>();

            await dbContext.SaveChangesAsync();

            return saleLog;
        }

        public async Task<int> ClearSaleLogs(Guid? saleId)
        {
            var sale = await salesEntityRetriever.GetSale(saleId);

            if (sale == null)
            {
                throw new NullReferenceException(SaleNotFound);
            }

            var saleLogs = dbContext.SaleLogs.Where(sl => sl.SaleId == sale.Id).ToList();

            var logsToBeClearedNumber = saleLogs.Count;

            dbContext.SaleLogs.RemoveRange(saleLogs);

            await dbContext.SaveChangesAsync();

            return logsToBeClearedNumber;
        }

        public async Task<IEnumerable<TModel>> GetLogsForSale<TModel>(Guid? saleId)
        {
            return await dbContext.SaleLogs.Where(sl => sl.SaleId == saleId).OrderBy(sl => sl.CreatedOn)
                .To<TModel>().ToListAsync();
        }

        private string GenerateLogMessage(Common.Enumerations.SaleLogs logType)
        {
            var logMessage = (string) typeof(SaleLogsMessages).GetField(logType.ToString()).GetValue(null);

            return logMessage;
        }
    }
}