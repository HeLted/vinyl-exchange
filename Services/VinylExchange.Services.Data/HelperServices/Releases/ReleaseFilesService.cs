namespace VinylExchange.Services.Data.HelperServices.Releases
{
    #region

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

    #endregion

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

        public async Task<List<ReleaseFile>> AddFilesForRelease(Guid? releaseId, Guid formSessionId)
        {
            var uploadFileUtilityModels = this.fileManager.RetrieveFilesFromCache(formSessionId);

            var filesContent = this.fileManager.GetFilesByteContent(uploadFileUtilityModels);

            var releaseFilesModels = this.fileManager.MapFilesToDbObjects<ReleaseFile>(
                uploadFileUtilityModels,
                releaseId,
                ForeignKeyForFile,
                EntityTableName).ToList();

            releaseFilesModels = this.fileManager.SaveFilesToServer(releaseFilesModels, filesContent).ToList();

            if (releaseFilesModels.Where(rf => rf.FileType == FileType.Image).FirstOrDefault() == null)
            {
                releaseFilesModels.Add(
                    new ReleaseFile
                        {
                            FileName = "defaultCoverArt.jpg",
                            FileType = FileType.Image,
                            IsPreview = true,
                            Path = @"\Releases\Default\",
                            ReleaseId = releaseId
                        });
            }

            await this.dbContext.ReleaseFiles.AddRangeAsync(releaseFilesModels);

            return releaseFilesModels;
        }

        public async Task<TModel> GetReleaseCoverArt<TModel>(Guid releaseId)
        {
            return await this.dbContext.ReleaseFiles
                       .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                       .OrderBy(rf => rf.CreatedOn).To<TModel>().FirstOrDefaultAsync();
        }

        public async Task<List<TModel>> GetReleaseImages<TModel>(Guid releaseId)
        {
            return await this.dbContext.ReleaseFiles
                       .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                       .OrderBy(rf => rf.CreatedOn).To<TModel>().ToListAsync();
        }

        public async Task<List<TModel>> GetReleaseTracks<TModel>(Guid releaseId)
        {
            return await this.dbContext.ReleaseFiles
                       .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Audio)
                       .OrderBy(rf => rf.CreatedOn).To<TModel>().ToListAsync();
        }
    }
}