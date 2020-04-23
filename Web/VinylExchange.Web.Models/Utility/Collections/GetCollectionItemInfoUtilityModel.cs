namespace VinylExchange.Web.Models.Utility.Collections
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class GetCollectionItemInfoUtilityModel : IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}