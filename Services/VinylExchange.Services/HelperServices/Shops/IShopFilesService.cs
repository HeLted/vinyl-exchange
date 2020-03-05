using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.ResourceModels.ShopFiles;

namespace VinylExchange.Services.Data.HelperServices.Shops
{
    public interface IShopFilesService
    {
        Task<IEnumerable<ShopFile>> AddFilesForShop(Guid releaseId, Guid formSessionId);
        Task<ShopFileResourceModel> GetShopMainPhoto(Guid shopId);
    }
}