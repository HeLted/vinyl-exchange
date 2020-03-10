namespace VinylExchange.Services.Data.HelperServices.Sales.SaleLogs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Web.Models.ResourceModels.SaleLogs;

    public interface ISaleLogsService
    {
        Task<AddLogToSaleResourceModel> AddLogToSale(Guid saleId, SaleLogs logType);

        Task<IEnumerable<GetLogsForSaleResourceModel>> GetLogsForSale(Guid saleId);
    }
}