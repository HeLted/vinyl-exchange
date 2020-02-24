using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Shops;
using VinylExchange.Models.ResourceModels.Shops;

namespace VinylExchange.Services.Data.MainServices.Shops
{
    public interface IShopsService
    {
        Task<Shop> CreateShop(CreateShopInputModel inputModel, Guid formSessionId);

        Task<IEnumerable<GetShopsResourceModel>> GetShops(string searchTerm, int shopsToSkip);
    }
}
