using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Addresses
{
    public class GetUserAddressesResourceModel : IMapFrom<Address>
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string PostalCode { get; set; }
        public string FullAddress { get; set; }
    }
}
