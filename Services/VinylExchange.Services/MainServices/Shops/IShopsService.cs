using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Shops;

namespace VinylExchange.Services.Data.MainServices.Shops
{
    public interface IShopsService
    {
        Task<Shop> CreateShop(CreateShopInputModel inputModel, Guid formSessionId);
    }
}
