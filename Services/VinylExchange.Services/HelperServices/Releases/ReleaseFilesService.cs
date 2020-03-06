using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Common.Enumerations;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.ResourceModels.ReleaseFiles;
using VinylExchange.Services.Files;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.HelperServices.Releases
{
    public class ReleaseFilesService : IReleaseFilesService
    {
        private const string EntityTableName = "Releases";
        private const string ForeignKeyForFile = "ReleaseId";

        private readonly VinylExchangeDbContext dbContext;
        private readonly IFileManager fileManager;

        public ReleaseFilesService(VinylExchangeDbContext dbContext,IFileManager fileManager)
        {
            this.dbContext = dbContext;
            this.fileManager = fileManager;
        }

        public async Task<IEnumerable<ReleaseFile>> AddFilesForRelease(Guid releaseId, Guid formSessionId)
        {

            var uploadFileUtilityModels = fileManager.RetrieveFilesFromCache(formSessionId);

            var filesContent = fileManager.GetFilesByteContent(uploadFileUtilityModels);

            var releaseFilesModels = fileManager
                .MapFilesToDbObjects<ReleaseFile>(uploadFileUtilityModels,
                releaseId, ForeignKeyForFile, EntityTableName);

            releaseFilesModels = fileManager.SaveFilesToServer<ReleaseFile>(releaseFilesModels, filesContent);

            await dbContext.ReleaseFiles.AddRangeAsync(releaseFilesModels);

            return releaseFilesModels;

        }

        public async Task<IEnumerable<ReleaseFileResourceModel>> GetReleaseTracks(Guid releaseId)
        {
            return await dbContext.ReleaseFiles
                 .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Audio)
                 .OrderBy(rf => rf.CreatedOn)
                 .To<ReleaseFileResourceModel>()
                 .ToListAsync();
        }


        public async Task<IEnumerable<ReleaseFileResourceModel>> GetReleaseImages(Guid releaseId)
        {
            return await dbContext.ReleaseFiles
                 .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                 .OrderBy(rf => rf.CreatedOn)
                 .To<ReleaseFileResourceModel>()
                 .ToListAsync();
        }

        public async Task<ReleaseFileResourceModel> GetReleaseCoverArt(Guid releaseId)
        {
            return await dbContext.ReleaseFiles
                 .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                 .OrderBy(rf => rf.CreatedOn)
                 .To<ReleaseFileResourceModel>()
                 .FirstOrDefaultAsync();
        }



    }
}
