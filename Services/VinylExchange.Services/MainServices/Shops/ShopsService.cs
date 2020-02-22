using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Shops;
using VinylExchange.Services.Data.HelperServices;
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

        public async Task<IEnumerable<GetShopsResourceModel>> GetReleases(string searchTerm, IEnumerable<int> filterStyleIds, int releasesToSkip)
        {

            List<GetShopsResourceModel> releases = null;

            var shopsQuariable = dbContext.Releases.AsQueryable();

            if (searchTerm != null)
            {
                shopsQuariable = shopsQuariable.Where(r => r.Artist.Contains(searchTerm) || r.Title.Contains(searchTerm));
            }

            releases = await shopsQuariable
            .Where(r => r.Styles.Any(s => filterStyleIds.Contains(s.StyleId)) || filterStyleIds.Count() == 0)
            .Skip(releasesToSkip)
            .Take(shopsToTake)
            .To<GetShopsResourceModel>()
            .ToListAsync();

            releases.ForEach(r =>
            {
                r.CoverArt = Task.Run(() => shopFilesService.GetShopCoverArt(r.Id)).Result;
            });

            return releases;
        }

    }
}
