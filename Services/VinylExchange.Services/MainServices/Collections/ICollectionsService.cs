namespace VinylExchange.Services.MainServices.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;
    using VinylExchange.Web.Models.InputModels.Collections;
    using VinylExchange.Web.Models.ResourceModels.Collections;
    using VinylExchange.Web.Models.Utility;

    public interface ICollectionsService
    {
        Task<CollectionItem> AddToCollection(AddToCollectionInputModel inputModel, Guid releaseId, Guid userId);

        Task<bool> DoesUserCollectionContainRelease(Guid releaseId, Guid userId);

        Task<GetCollectionItemResourceModel> GetCollectionItem(Guid collectionItemId);

        Task<GetCollectionItemInfoUtilityModel> GetCollectionItemInfo(Guid collectionItemId);

        Task<IEnumerable<GetUserCollectionResourceModel>> GetUserCollection(Guid userId);

        Task<RemoveCollectionItemResourceModel> RemoveCollectionItem(Guid collectionItemId);
    }
}