namespace VinylExchange.Web.Models.Utility.Collections
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class GetCollectionItemInfoUtilityModel : IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}