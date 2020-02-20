using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.Utility
{
    public class GetCollectionItemInfoUtilityModel : IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
    }
}
