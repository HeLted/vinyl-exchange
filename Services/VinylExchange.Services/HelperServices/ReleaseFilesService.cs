using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.Utility;
using VinylExchange.Services.Files;
using VinylExchange.Services.MemoryCache;

namespace VinylExchange.Services.HelperServices
{
    public class ReleaseFilesService : IReleaseFilesService
    {
        private readonly VinylExchangeDbContext dbContext;
        private readonly MemoryCacheManager cacheManager;
        private readonly IFileManager fileManager;
       
        public ReleaseFilesService(VinylExchangeDbContext dbContext,
            MemoryCacheManager cacheManager, 
            IFileManager fileManager)
        {
            this.dbContext = dbContext;
            this.cacheManager = cacheManager;
            this.fileManager = fileManager;           
        }
        public async Task<IEnumerable<ReleaseFile>> AddFilesForRelease(Guid releaseId,string formSessionId)
        {
                       
            var uploadFileUtilityModels = fileManager.RetrieveFilesFromCache(formSessionId);

            var filesContent = fileManager.GetFilesByteContent(uploadFileUtilityModels);

            var releaseFilesModels = fileManager
                .MapFilesToDbObjects<ReleaseFile>(uploadFileUtilityModels,
                releaseId,"ReleaseId" ,"Releases");

            releaseFilesModels = fileManager.SaveFilesToServer<ReleaseFile>(releaseFilesModels, filesContent);
            
            await dbContext.ReleaseFiles.AddRangeAsync(releaseFilesModels);

            await dbContext.SaveChangesAsync();

            return releaseFilesModels;
           
        }
        
    }
}
