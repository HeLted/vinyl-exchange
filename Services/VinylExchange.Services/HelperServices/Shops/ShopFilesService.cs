using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Common.Enumerations;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.ResourceModels.ShopFiles;
using VinylExchange.Services.Files;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.Data.HelperServices.Shops
{
    public class ShopFilesService : IShopFilesService
    {
        private const string EntityTableName = "Shops";
        private const string ForeignKeyForFile = "ShopId";

        private readonly VinylExchangeDbContext dbContext;
        private readonly IFileManager fileManager;

        public ShopFilesService(VinylExchangeDbContext dbContext, IFileManager fileManager)
        {
            this.dbContext = dbContext;
            this.fileManager = fileManager;
        }

        public async Task<IEnumerable<ShopFile>> AddFilesForShop(Guid releaseId, Guid formSessionId)
        {

            var uploadFileUtilityModels = fileManager.RetrieveFilesFromCache(formSessionId);

            var filesContent = fileManager.GetFilesByteContent(uploadFileUtilityModels);

            var shopFilesModels = fileManager
                .MapFilesToDbObjects<ShopFile>(uploadFileUtilityModels,
                releaseId, ForeignKeyForFile, EntityTableName);

            shopFilesModels = fileManager.SaveFilesToServer<ShopFile>(shopFilesModels, filesContent);

            await dbContext.ShopFiles.AddRangeAsync(shopFilesModels);

            return shopFilesModels;

        }

        public async Task<IEnumerable<ShopFileResourceModel>> GetReleaseImages(Guid releaseId)
        {
            return await dbContext.ReleaseFiles
                 .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                 .OrderBy(rf => rf.CreatedOn)
                 .To<ShopFileResourceModel>()
                 .ToListAsync();
        }

        public async Task<ShopFileResourceModel> GetShopMainPhoto(Guid shopId)
        {
            return await dbContext.ShopFiles
                 .Where(sf => sf.ShopId == shopId && sf.FileType == FileType.Image)
                 .OrderBy(rf => rf.CreatedOn)
                 .To<ShopFileResourceModel>()
                 .FirstOrDefaultAsync();
        }

    }
}
