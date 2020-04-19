namespace VinylExchange.Services.Data.HelperServices.Sales.SaleLogs
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Web.Models.ResourceModels.SaleLogs;

    #endregion

    public interface ISaleLogsService
    {
        Task<TModel> AddLogToSale<TModel>(Guid? saleId, SaleLogs logType);

        Task<int> ClearSaleLogs(Guid? saleId);

        Task<IEnumerable<TModel>> GetLogsForSale<TModel>(Guid? saleId);
    }
}