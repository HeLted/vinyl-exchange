namespace VinylExchange.Web.Models.ResourceModels.Sales
{
    #region

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class SaleStatusResourceModel : IMapFrom<Sale>
    {
        public Status Status { get; set; }
    }
}