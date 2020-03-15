namespace VinylExchange.Services.HelperServices.Releases
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;
    using VinylExchange.Web.Models.ResourceModels.ReleaseFiles;

    public interface IReleaseFilesService
    {
        Task<List<ReleaseFile>> AddFilesForRelease(Guid releaseId, Guid formSessionId);

        Task<ReleaseFileResourceModel> GetReleaseCoverArt(Guid releaseId);

        Task<List<ReleaseFileResourceModel>> GetReleaseImages(Guid releaseId);

        Task<List<ReleaseFileResourceModel>> GetReleaseTracks(Guid releaseId);
    }
}