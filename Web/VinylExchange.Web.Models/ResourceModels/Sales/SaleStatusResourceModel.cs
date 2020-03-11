using VinylExchange.Data.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.Sales
{
    public  class SaleStatusResourceModel : IMapFrom<Sale>
    {
        public Status Status { get; set; }
    }
}
