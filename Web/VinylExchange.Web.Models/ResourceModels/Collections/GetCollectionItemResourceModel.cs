namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    #region

    using System;

    using VinylExchange.Data.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class GetCollectionItemResourceModel : IMapFrom<CollectionItem>
    {
        public string Description { get; set; }

        public Guid Id { get; set; }

        public Condition SleeveGrade { get; set; }

        public Condition VinylGrade { get; set; }
    }
}