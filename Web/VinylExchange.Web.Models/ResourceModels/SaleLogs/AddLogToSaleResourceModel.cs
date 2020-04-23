namespace VinylExchange.Web.Models.ResourceModels.SaleLogs
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class AddLogToSaleResourceModel : IMapFrom<SaleLog>
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid Id { get; set; }
    }
}