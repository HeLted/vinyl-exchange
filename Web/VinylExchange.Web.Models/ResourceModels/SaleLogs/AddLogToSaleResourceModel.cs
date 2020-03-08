using System;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.SaleLogs
{
    public class AddLogToSaleResourceModel : IMapFrom<SaleLog>
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
