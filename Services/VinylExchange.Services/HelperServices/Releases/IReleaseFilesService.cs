namespace VinylExchange.Services.HelperServices.Releases
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;
    using VinylExchange.Web.Models.ResourceModels.ReleaseFiles;

    public interface IReleaseFilesService
    {
        Task<IEnumerable<ReleaseFile>> AddFilesForRelease(Guid releaseId, Guid formSessionId);

        Task<ReleaseFileResourceModel> GetReleaseCoverArt(Guid releaseId);

        Task<IEnumerable<ReleaseFileResourceModel>> GetReleaseImages(Guid releaseId);

        Task<IEnumerable<ReleaseFileResourceModel>> GetReleaseTracks(Guid releaseId);
    }
}