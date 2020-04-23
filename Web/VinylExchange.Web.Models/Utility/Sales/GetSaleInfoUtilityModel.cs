namespace VinylExchange.Web.Models.Utility.Sales
{
    using System;
    using Data.Common.Enumerations;
    using Data.Models;
    using Services.Mapping;

    public class GetSaleInfoUtilityModel : IMapFrom<Sale>
    {
        public Guid? BuyerId { get; set; }

        public Guid Id { get; set; }

        public Guid? ReleaseId { get; set; }

        public Guid? SellerId { get; set; }

        public Status Status { get; set; }
    }
}