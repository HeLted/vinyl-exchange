using System;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    public class GetCollectionItemUserIdResourceModel : IMapFrom<CollectionItem>
    {
        public Guid UserId { get; set; }
    }
}
