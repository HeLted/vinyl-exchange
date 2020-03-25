namespace VinylExchange.Services.Data.MainServices.Collections
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Collections;

    #endregion

    public class CollectionsService : ICollectionsService
    {
        private readonly VinylExchangeDbContext dbContext;

        public CollectionsService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TModel> AddToCollection<TModel>(
            AddToCollectionInputModel inputModel,
            Guid releaseId,
            Guid userId)
        {
            var collectionItem = inputModel.To<CollectionItem>();

            collectionItem.ReleaseId = releaseId;
            collectionItem.UserId = userId;

            var trackedCollectionItem = await this.dbContext.Collections.AddAsync(collectionItem);

            await this.dbContext.SaveChangesAsync();

            return trackedCollectionItem.Entity.To<TModel>();
        }

        public async Task<bool> DoesUserCollectionContainRelease(Guid releaseId, Guid userId)
        {
            return await this.dbContext.Collections.Where(ci => ci.ReleaseId == releaseId && ci.UserId == userId)
                       .CountAsync() > 0;
        }

        public async Task<TModel> GetCollectionItem<TModel>(Guid collectionItemId)
        {
            return await this.dbContext.Collections.Where(ci => ci.Id == collectionItemId).To<TModel>()
                       .FirstOrDefaultAsync();
        }

        public async Task<TModel> GetCollectionItemInfo<TModel>(Guid collectionItemId)
        {
            return await this.dbContext.Collections.Where(ci => ci.Id == collectionItemId).To<TModel>()
                       .FirstOrDefaultAsync();
        }

        public async Task<List<TModel>> GetUserCollection<TModel>(Guid userId)
        {
            return await this.dbContext.Collections.Where(ci => ci.UserId == userId).To<TModel>().ToListAsync();
        }

        public async Task<TModel> RemoveCollectionItem<TModel>(Guid collectionItemId)
        {
            var collectionItem = await this.dbContext.Collections.FirstOrDefaultAsync(ci => ci.Id == collectionItemId);

            if (collectionItem == null)
            {
                throw new NullReferenceException("Collection item with this Id doesn't exist");
            }

            this.dbContext.Collections.Remove(collectionItem);
            await this.dbContext.SaveChangesAsync();

            return collectionItem.To<TModel>();
        }
    }
}