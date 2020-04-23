namespace VinylExchange.Web.Models.ResourceModels.Sales
{
    using Data.Common.Enumerations;
    using Data.Models;
    using Services.Mapping;

    public class SaleStatusResourceModel : IMapFrom<Sale>
    {
        public Status Status { get; set; }
    }
}