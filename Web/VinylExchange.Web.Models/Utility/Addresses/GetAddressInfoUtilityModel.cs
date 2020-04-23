namespace VinylExchange.Web.Models.Utility.Addresses
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class GetAddressInfoUtilityModel : IMapFrom<Address>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}