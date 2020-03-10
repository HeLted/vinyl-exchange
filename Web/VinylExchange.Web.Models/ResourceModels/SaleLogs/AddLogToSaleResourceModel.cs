namespace VinylExchange.Web.Models.ResourceModels.SaleLogs
{
    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class AddLogToSaleResourceModel : IMapFrom<SaleLog>
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid Id { get; set; }
    }
}