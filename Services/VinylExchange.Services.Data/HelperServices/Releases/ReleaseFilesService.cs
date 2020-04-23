namespace VinylExchange.Services.Data.HelperServices.Releases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Enumerations;
    using Files;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;

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
            var uploadFileUtilityModels = fileManager.RetrieveFilesFromCache(formSessionId);

            var filesContent = fileManager.GetFilesByteContent(uploadFileUtilityModels);

            var releaseFilesModels = fileManager.MapFilesToDbObjects<ReleaseFile>(
                uploadFileUtilityModels,
                releaseId,
                ForeignKeyForFile,
                EntityTableName).ToList();

            releaseFilesModels = fileManager.SaveFilesToServer(releaseFilesModels, filesContent).ToList();

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

            await dbContext.ReleaseFiles.AddRangeAsync(releaseFilesModels);

            return releaseFilesModels;
        }

        public async Task<TModel> GetReleaseCoverArt<TModel>(Guid? releaseId)
        {
            return await dbContext.ReleaseFiles
                .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                .OrderBy(rf => rf.CreatedOn).To<TModel>().FirstOrDefaultAsync();
        }

        public async Task<List<TModel>> GetReleaseImages<TModel>(Guid? releaseId)
        {
            return await dbContext.ReleaseFiles
                .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Image)
                .OrderBy(rf => rf.CreatedOn).To<TModel>().ToListAsync();
        }

        public async Task<List<TModel>> GetReleaseTracks<TModel>(Guid? releaseId)
        {
            return await dbContext.ReleaseFiles
                .Where(rf => rf.ReleaseId == releaseId && rf.FileType == FileType.Audio)
                .OrderBy(rf => rf.CreatedOn).To<TModel>().ToListAsync();
        }
    }
}