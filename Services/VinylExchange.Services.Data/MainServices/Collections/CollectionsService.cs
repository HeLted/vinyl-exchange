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
    using VinylExchange.Services.Data.MainServices.Releases;
    using VinylExchange.Services.Data.MainServices.Users;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Collections;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    public class CollectionsService : ICollectionsService
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IReleasesEntityRetriever releasesEntityRetriever;

        private readonly IUsersEntityRetriever usersEntityRetriever;

        public CollectionsService(
            VinylExchangeDbContext dbContext,
            IReleasesEntityRetriever releasesEntityRetriever,
            IUsersEntityRetriever usersEntityRetriever)
        {
            this.dbContext = dbContext;
            this.releasesEntityRetriever = releasesEntityRetriever;
            this.usersEntityRetriever = usersEntityRetriever;
        }

        public async Task<TModel> AddToCollection<TModel>(AddToCollectionInputModel inputModel, Guid userId)
        {
            var release = await this.releasesEntityRetriever.GetRelease(inputModel.ReleaseId);

            if (release == null)
            {
                throw new NullReferenceException(ReleaseNotFound);
            }

            var user = await this.usersEntityRetriever.GetUser(userId);

            if (user == null)
            {
                throw new NullReferenceException(UserNotFound);
            }

            var collectionItem = inputModel.To<CollectionItem>();

            collectionItem.ReleaseId = release.Id;
            collectionItem.UserId = user.Id;

            var trackedCollectionItem = await this.dbContext.Collections.AddAsync(collectionItem);

            await this.dbContext.SaveChangesAsync();

            return trackedCollectionItem.Entity.To<TModel>();
        }

        public async Task<TModel> RemoveCollectionItem<TModel>(Guid? collectionItemId)
        {
            var collectionItem = await this.dbContext.Collections.FirstOrDefaultAsync(ci => ci.Id == collectionItemId);

            if (collectionItem == null)
            {
                throw new NullReferenceException(CollectionItemNotFound);
            }

            this.dbContext.Collections.Remove(collectionItem);
            await this.dbContext.SaveChangesAsync();

            return collectionItem.To<TModel>();
        }

        public async Task<List<TModel>> GetUserCollection<TModel>(Guid userId)
        {
            return await this.dbContext.Collections.Where(ci => ci.UserId == userId).To<TModel>().ToListAsync();
        }

        public async Task<TModel> GetCollectionItem<TModel>(Guid? collectionItemId)
        {
            return await this.dbContext.Collections.Where(ci => ci.Id == collectionItemId).To<TModel>()
                       .FirstOrDefaultAsync();
        }

        public async Task<bool> DoesUserCollectionContainRelease(Guid? releaseId, Guid userId)
        {
            return await this.dbContext.Collections.Where(ci => ci.ReleaseId == releaseId && ci.UserId == userId)
                       .CountAsync() > 0;
        }
    }
}