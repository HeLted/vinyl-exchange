namespace VinylExchange.Web.Models.ResourceModels.Collections
{
    #region

    using System;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class RemoveCollectionItemResourceModel : IMapFrom<CollectionItem>
    {
        public Guid Id { get; set; }
    }
}