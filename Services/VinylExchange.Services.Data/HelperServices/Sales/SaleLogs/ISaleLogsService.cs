﻿namespace VinylExchange.Services.Data.HelperServices.Sales.SaleLogs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Enumerations;

    public interface ISaleLogsService
    {
        Task<TModel> AddLogToSale<TModel>(Guid? saleId, SaleLogs logType);

        Task<int> ClearSaleLogs(Guid? saleId);

        Task<IEnumerable<TModel>> GetLogsForSale<TModel>(Guid? saleId);
    }
}