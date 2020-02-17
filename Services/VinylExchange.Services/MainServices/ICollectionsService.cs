using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Collections;

namespace VinylExchange.Services.MainServices
{
    public interface ICollectionsService
    {
        Task<CollectionItem> AddToCollection(AddToCollectionInputModel inputModel, Guid releaseId, string userId);
    }
}
