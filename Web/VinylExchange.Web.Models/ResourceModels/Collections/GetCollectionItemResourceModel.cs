namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    using System;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    public class GetCollectionItemResourceModel : IMapFrom<CollectionItem>
    {
        public string Description { get; set; }

        public Guid Id { get; set; }

        public Condition SleeveGrade { get; set; }

        public Condition VinylGrade { get; set; }
    }
}