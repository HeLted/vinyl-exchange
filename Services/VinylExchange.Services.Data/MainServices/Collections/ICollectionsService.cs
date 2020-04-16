﻿namespace VinylExchange.Services.Data.MainServices.Collections
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.InputModels.Collections;

    #endregion

    public interface ICollectionsService
    {
        Task<TModel> AddToCollection<TModel>(AddToCollectionInputModel inputModel, Guid userId);

        Task<bool> DoesUserCollectionContainRelease(Guid? releaseId, Guid userId);

        Task<TModel> GetCollectionItem<TModel>(Guid? collectionItemId);

        Task<List<TModel>> GetUserCollection<TModel>(Guid userId);

        Task<TModel> RemoveCollectionItem<TModel>(Guid? collectionItemId);
    }
}