namespace VinylExchange.Services.HelperServices.Releases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Files;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.ReleaseFiles;
    using VinylExchange.Web.Models.Utility;

    public class ReleaseFilesService : IReleaseFilesService
    {
        private const string EntityTableName = "Releases";

        private const string ForeignKeyForFile = "ReleaseId";

        private readonly VinylExchangeDbContext dbContext;

        private readonly IFileManager fileManager;

        public ReleaseFilesService(VinylExchangeDbContext dbContext, IFileManager fileManager)
        {
            this.dbContext = dbContext;
            this.fileManager = fileManager;
        }

        public async Task<List<ReleaseFile>> AddFilesForRelease(Guid releaseId, Guid formSessionId)
        {
            IEnumerable<UploadFileUtilityModel> uploadFileUtilityModels =
                this.fileManager.RetrieveFilesFromCache(formSessionId);

            IEnumerable<byte[]> filesContent = this.fileManager.GetFilesByteContent(uploadFileUtilityModels);

            List<ReleaseFile> releaseFilesModels = this.fileManager.MapFilesToDbObjects<ReleaseFile>(
                uploadFileUtilityModels,
                releaseId,
                ForeignKeyForFile,
                EntityTableName).ToList();

            releaseFilesModels = this.fileManager.SaveFilesToServer(releaseFilesModels, filesContent).ToList();

            await this.dbContext.ReleaseFiles.AddRangeAsync(releaseFilesModels);

            return releaseFilesModels;
        }

        public async Task<ReleaseFileResourceModel> GetReleaseCoverArt(Guid releaseId)
          => await this.dbContext.ReleaseFiles
                       .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                       .OrderBy(rf => rf.CreatedOn).To<ReleaseFileResourceModel>().FirstOrDefaultAsync();
        
        public async Task<List<ReleaseFileResourceModel>> GetReleaseImages(Guid releaseId)
          => await this.dbContext.ReleaseFiles
                       .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                       .OrderBy(rf => rf.CreatedOn).To<ReleaseFileResourceModel>().ToListAsync();
       

        public async Task<List<ReleaseFileResourceModel>> GetReleaseTracks(Guid releaseId)
          => await this.dbContext.ReleaseFiles
                       .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Audio)
                       .OrderBy(rf => rf.CreatedOn).To<ReleaseFileResourceModel>().ToListAsync();
        
    }
}