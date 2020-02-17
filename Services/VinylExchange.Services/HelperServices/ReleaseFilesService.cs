using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.Utility;
using VinylExchange.Models.ViewModels.ReleaseFiles;
using VinylExchange.Services.Files;
using VinylExchange.Services.MemoryCache;
using VinylExchange.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using VinylExchange.Common.Enumerations;

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
        public async Task<IEnumerable<ReleaseFile>> AddFilesForRelease(Guid releaseId,Guid formSessionId)
        {
                       
            var uploadFileUtilityModels = fileManager.RetrieveFilesFromCache(formSessionId.ToString());

            var filesContent = fileManager.GetFilesByteContent(uploadFileUtilityModels);

            var releaseFilesModels = fileManager
                .MapFilesToDbObjects<ReleaseFile>(uploadFileUtilityModels,
                releaseId,"ReleaseId" ,"Releases");

            releaseFilesModels = fileManager.SaveFilesToServer<ReleaseFile>(releaseFilesModels, filesContent);
            
            await dbContext.ReleaseFiles.AddRangeAsync(releaseFilesModels);
                      
            return releaseFilesModels;
           
        }

        public async Task<IEnumerable<ReleaseFileViewModel>> GetReleaseTracks(Guid releaseId)
        {
           return await dbContext.ReleaseFiles
                .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Audio)
                .OrderBy(rf => rf.CreatedOn)
                .To<ReleaseFileViewModel>()
                .ToListAsync();
        }


        public async Task<IEnumerable<ReleaseFileViewModel>> GetReleaseImages(Guid releaseId)
        {
            return await dbContext.ReleaseFiles
                 .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                 .OrderBy(rf => rf.CreatedOn)
                 .To<ReleaseFileViewModel>()
                 .ToListAsync();
        }

        public async Task<ReleaseFileViewModel> GetReleaseCoverArt(Guid releaseId)
        {
            return await dbContext.ReleaseFiles
                 .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                 .OrderBy(rf => rf.CreatedOn)
                 .To<ReleaseFileViewModel>()
                 .FirstOrDefaultAsync();
        }


     

    }
}
