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
using VinylExchange.Services.Mapping;
using VinylExchange.Data.Common.Enumerations;


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


        public async Task<CollectionItem> AddToCollection(AddToCollectionInputModel inputModel, Guid releaseId, Guid userId)
        {
            var collectionItem = new CollectionItem()
            {
                
                VinylGrade = inputModel.VinylGrade,
                SleeveGrade = inputModel.SleeveGrade,
                Description = inputModel.Description,
                ReleaseId = releaseId,
                UserId = userId
            };

            var trackedCollectionItem = await this.dbContext.Collections.AddAsync(collectionItem);

            await this.dbContext.SaveChangesAsync();

            return trackedCollectionItem.Entity;

        }

        public async Task<IEnumerable<GetUserCollectionResourceModel>> GetUserCollection(Guid userId)
        {
            var collectionItems = await this.dbContext.Collections.Where(ci => ci.UserId == userId).To<GetUserCollectionResourceModel>().ToListAsync();

            collectionItems.ForEach(ci =>
            {
                ci.CoverArt = this.releaseFileService.GetReleaseCoverArt(ci.ReleaseId).GetAwaiter().GetResult();
            });

            return collectionItems;
        }

        public async Task<GetCollectionItemInfoUtilityModel> GetCollectionItemInfo(Guid collectionItemId)
        {
            var collectionItem = await this.dbContext.Collections
                .Where(ci => ci.Id == collectionItemId)
                .To<GetCollectionItemInfoUtilityModel>()
                .FirstOrDefaultAsync();

            return collectionItem;
        }

        public async Task<RemoveCollectionItemResourceModel> RemoveCollectionItem(Guid collectionItemId)
        {
            var collectionItem = await this.dbContext.Collections.FirstOrDefaultAsync(ci => ci.Id == collectionItemId);
            this.dbContext.Collections.Remove(collectionItem);
            await this.dbContext.SaveChangesAsync();
            return new RemoveCollectionItemResourceModel()
            {
                Id = collectionItem.Id,
            };
        }

        public async Task<bool> DoesUserCollectionContainReleas(Guid releaseId, Guid userId)
        => await this.dbContext.Collections
                .Where(ci => ci.ReleaseId == releaseId && ci.UserId == userId)
                .CountAsync() > 0;


    }

}



