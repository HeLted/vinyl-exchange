using System;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.Addresses
{
    public class CreateAddressResourceModel :IMapFrom<Address>
    {

        public Guid Id { get; set; }

    }
}
