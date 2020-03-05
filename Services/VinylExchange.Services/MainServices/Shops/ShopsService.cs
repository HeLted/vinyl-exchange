using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Shops;
using VinylExchange.Models.ResourceModels.Shops;
using VinylExchange.Services.Data.HelperServices;
using VinylExchange.Services.Data.HelperServices.Shops;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.Data.MainServices.Shops
{
    public class ShopsService : IShopsService
    {

        private const int shopsToTake = 6;

        private readonly VinylExchangeDbContext dbContext;
        private readonly IShopFilesService shopFilesService;

        public ShopsService(VinylExchangeDbContext dbContext,IShopFilesService shopFilesService)
        {
            this.dbContext = dbContext;
            this.shopFilesService = shopFilesService;
        }

        public async Task<Shop> CreateShop(CreateShopInputModel inputModel, Guid formSessionId)
        {
            var shop = inputModel.To<Shop>();

            var trackedShop = await this.dbContext.Shops.AddAsync(shop);

            await shopFilesService.AddFilesForShop(shop.Id, formSessionId);

            await dbContext.SaveChangesAsync();

            return trackedShop.Entity;
        }

        public async Task<IEnumerable<GetShopsResourceModel>> GetShops(string searchTerm,  int shopsToSkip)
        {

            List<GetShopsResourceModel> releases = null;

            var shopsQuariable = dbContext.Shops.AsQueryable();

            if (searchTerm != null)
            {
                shopsQuariable = shopsQuariable.Where(r => r.Name.Contains(searchTerm));
            }

            releases = await shopsQuariable          
            .Skip(shopsToSkip)
            .Take(shopsToTake)
            .To<GetShopsResourceModel>()
            .ToListAsync();

            releases.ForEach(r =>
            {
                r.MainPhoto = Task.Run(() => shopFilesService.GetShopMainPhoto(r.Id)).Result;
            });

            return releases;
        }

    }
}
