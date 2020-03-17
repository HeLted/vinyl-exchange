namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class AddToCollectionResourceModel : IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }
    }
}