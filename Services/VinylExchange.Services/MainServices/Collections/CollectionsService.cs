using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Collections;
using VinylExchange.Models.ResourceModels.Collections;
using VinylExchange.Models.Utility;
using VinylExchange.Services.HelperServices;
using VinylExchange.Services.HelperServices.Releases;
using VinylExchange.Services.Mapping;


namespace VinylExchange.Services.MainServices.Collections
{
    public class CollectionsService : ICollectionsService
    {
        private readonly VinylExchangeDbContext dbContext;
        private readonly IReleaseFilesService releaseFileService;

        public CollectionsService(VinylExchangeDbContext dbContext, IReleaseFilesService releaseFileService)
        {
            this.dbContext = dbContext;
            this.releaseFileService = releaseFileService;
        }


        public async Task<GetCollectionItemResourceModel> GetCollectionItem(Guid collectionItemId) 
            => await this.dbContext.Collections
                .Where(ci => ci.Id == collectionItemId)
                .To<GetCollectionItemResourceModel>()
                .FirstOrDefaultAsync();
 


        public async Task<CollectionItem> AddToCollection(AddToCollectionInputModel inputModel, Guid releaseId, Guid userId)
        {

            var collectionItem = inputModel.To<CollectionItem>();

            collectionItem.ReleaseId = releaseId;
            collectionItem.UserId = userId;

            var trackedCollectionItem = await this.dbContext.Collections.AddAsync(collectionItem);

            await this.dbContext.SaveChangesAsync();

            return trackedCollectionItem.Entity;

        }

        public async Task<IEnumerable<GetUserCollectionResourceModel>> GetUserCollection(Guid userId)
        {
            var collectionItems = await this.dbContext.Collections
                .Where(ci => ci.UserId == userId)
                .To<GetUserCollectionResourceModel>()
                .ToListAsync();

            collectionItems.ForEach(ci =>
            {
                ci.CoverArt = this.releaseFileService.GetReleaseCoverArt(ci.ReleaseId).GetAwaiter().GetResult();
            });

            return collectionItems;
        }

        public async Task<GetCollectionItemInfoUtilityModel> GetCollectionItemInfo(Guid collectionItemId)
            => await this.dbContext.Collections
                .Where(ci => ci.Id == collectionItemId)
                .To<GetCollectionItemInfoUtilityModel>()
                .FirstOrDefaultAsync();



        public async Task<RemoveCollectionItemResourceModel> RemoveCollectionItem(Guid collectionItemId)
        {
            var collectionItem = await this.dbContext.Collections.FirstOrDefaultAsync(ci => ci.Id == collectionItemId);
           
            if(collectionItem == null)
            {
                throw new NullReferenceException("Collection item with this Id doesn't exist");
            }

            this.dbContext.Collections.Remove(collectionItem);
            await this.dbContext.SaveChangesAsync();
            var resourceModel = collectionItem.To<RemoveCollectionItemResourceModel>();

            return resourceModel;
        }

        public async Task<bool> DoesUserCollectionContainRelease(Guid releaseId, Guid userId)
        => await this.dbContext.Collections
                .Where(ci => ci.ReleaseId == releaseId && ci.UserId == userId)
                .CountAsync() > 0;


    }

}



