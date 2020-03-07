namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class RemoveCollectionItemResourceModel : IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }
    }
}