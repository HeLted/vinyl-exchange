namespace VinylExchange.Models.Utility
{
    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class GetCollectionItemInfoUtilityModel : IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}