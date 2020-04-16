namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class GetCollectionItemUserIdResourceModel : IMapFrom<CollectionItem>
    {
        public Guid UserId { get; set; }
    }
}