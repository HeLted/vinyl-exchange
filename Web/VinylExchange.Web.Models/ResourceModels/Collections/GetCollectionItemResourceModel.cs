namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    using System;
    using Data.Common.Enumerations;
    using Data.Models;
    using Services.Mapping;

    public class GetCollectionItemResourceModel : IMapFrom<CollectionItem>
    {
        public string Description { get; set; }

        public Guid Id { get; set; }

        public Condition SleeveGrade { get; set; }

        public Condition VinylGrade { get; set; }
    }
}