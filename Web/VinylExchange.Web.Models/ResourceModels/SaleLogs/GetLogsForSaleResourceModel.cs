using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.SaleLogs
{
    public class GetLogsForSaleResourceModel : IMapFrom <SaleLog>
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
