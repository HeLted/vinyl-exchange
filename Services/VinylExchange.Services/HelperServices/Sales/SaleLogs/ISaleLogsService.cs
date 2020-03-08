namespace VinylExchange.Services.Data.HelperServices.Sales.SaleLogs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Web.Models.ResourceModels.SaleLogs;
    using VinylExchange.Web.Models.ResourceModels.SaleMessages;
    using VinylExchange.Common.Enumerations;

    public interface ISaleLogsService
    {
        Task<AddLogToSaleResourceModel> AddLogToSale(Guid saleId, SaleLogs logType);

        Task<IEnumerable<GetLogsForSaleResourceModel>> GetLogsForSale(Guid saleId);
    }
}