using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Collections;

namespace VinylExchange.Services.MainServices
{
    public class CollectionsService : ICollectionsService
    {
        private readonly VinylExchangeDbContext dbContext;

        public CollectionsService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;

        }


        public async Task<CollectionItem> AddToCollection(AddToCollectionInputModel inputModel, Guid releaseId, string userId)
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
    }

}



