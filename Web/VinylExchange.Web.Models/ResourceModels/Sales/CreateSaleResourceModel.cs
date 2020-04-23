namespace VinylExchange.Web.Models.ResourceModels.Sales
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class CreateSaleResourceModel : IMapFrom<Sale>
    {
        public Guid Id { get; set; }
    }
}