using System;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.Utility
{
    public  class GetAddressInfoUtilityModel :IMapFrom<Address>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
