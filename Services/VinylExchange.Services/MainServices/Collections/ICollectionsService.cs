using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Collections;
using VinylExchange.Models.ResourceModels.Collections;
using VinylExchange.Models.Utility;

namespace VinylExchange.Services.MainServices.Collections
{
    public interface ICollectionsService
    {
        Task<GetCollectionItemResourceModel> GetCollectionItem(Guid collectionItemId);
        Task<CollectionItem> AddToCollection(AddToCollectionInputModel inputModel, Guid releaseId, Guid userId);
        Task<IEnumerable<GetUserCollectionResourceModel>> GetUserCollection(Guid userId);
        Task<GetCollectionItemInfoUtilityModel> GetCollectionItemInfo(Guid collectionItemId);
        Task<RemoveCollectionItemResourceModel> RemoveCollectionItem(Guid collectionItemId);
        Task<bool> DoesUserCollectionContainRelease(Guid releaseId, Guid userId);
    }
}
