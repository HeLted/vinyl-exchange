namespace VinylExchange.Services.Data.MainServices.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Data.Common.Enumerations;

    public interface ICollectionsService
    {
        Task<TModel> AddToCollection<TModel>(
            Condition vinylGrade,
            Condition sleeveGrade,
            string description,
            Guid? releaseId,
            Guid userId);

        Task<bool> DoesUserCollectionContainRelease(Guid? releaseId, Guid userId);

        Task<TModel> GetCollectionItem<TModel>(Guid? collectionItemId);

        Task<List<TModel>> GetUserCollection<TModel>(Guid userId);

        Task<TModel> RemoveCollectionItem<TModel>(Guid? collectionItemId);
    }
}