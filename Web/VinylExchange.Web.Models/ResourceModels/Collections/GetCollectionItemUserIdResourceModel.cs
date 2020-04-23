namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class GetCollectionItemUserIdResourceModel : IMapFrom<CollectionItem>
    {
        public Guid UserId { get; set; }
    }
}