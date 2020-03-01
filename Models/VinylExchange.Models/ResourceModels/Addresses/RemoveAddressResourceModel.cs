using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Addresses
{
    public class RemoveAddressResourceModel : IMapFrom<Address>
    {
        public Guid Id { get; set; }
    }
}
