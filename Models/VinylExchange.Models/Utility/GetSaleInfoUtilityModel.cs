namespace VinylExchange.Models.Utility
{
    using System;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class GetSaleInfoUtilityModel : IMapFrom<Sale>
    {
        public Guid? BuyerId { get; set; }

        public Guid Id { get; set; }

        public Guid? ReleaseId { get; set; }

        public Guid? SellerId { get; set; }

        public Status Status { get; set; }
    }
}