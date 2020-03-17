namespace VinylExchange.Web.Models.ResourceModels.SaleLogs
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class GetLogsForSaleResourceModel : IMapFrom<SaleLog>
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid Id { get; set; }
    }
}