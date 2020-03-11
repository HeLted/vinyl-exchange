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
        Task<TModel> AddToCollection<TModel>(AddToCollectionInputModel inputModel, Guid releaseId, Guid userId);

        Task<bool> DoesUserCollectionContainRelease(Guid releaseId, Guid userId);

        Task<TModel> GetCollectionItem<TModel>(Guid collectionItemId);

        Task<TModel> GetCollectionItemInfo<TModel>(Guid collectionItemId);

        Task<List<TModel>> GetUserCollection<TModel>(Guid userId);

        Task<TModel> RemoveCollectionItem<TModel>(Guid collectionItemId);
    }
}