using System;
using VinylExchange.Data.Models;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Models.ResourceModels.Collections
{
    public class RemoveCollectionItemResourceModel : IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }
    }
}
