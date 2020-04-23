namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class AddToCollectionResourceModel : IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }
    }
}