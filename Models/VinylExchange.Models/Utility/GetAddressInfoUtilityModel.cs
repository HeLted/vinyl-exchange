namespace VinylExchange.Models.Utility
{
    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class GetAddressInfoUtilityModel : IMapFrom<Address>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}