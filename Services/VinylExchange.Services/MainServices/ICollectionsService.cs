using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Collections;
using VinylExchange.Models.ResourceModels.Collections;
using VinylExchange.Models.Utility;

namespace VinylExchange.Services.MainServices
{
    public interface ICollectionsService
    {
        Task<CollectionItem> AddToCollection(AddToCollectionInputModel inputModel, Guid releaseId, string userId);
        Task<IEnumerable<GetUserCollectionResourceModel>> GetUserCollection(string userId);
        Task<GetCollectionItemInfoUtilityModel> GetCollectionItemInfo(Guid collectionItemId);
        Task<RemoveCollectionItemResourceModel> RemoveCollectionItem(Guid collectionItemId);
        Task<bool> DoesUserCollectionContainReleas(Guid releaseId, string userId);
    }
}
