using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.ResourceModels.ReleaseFiles;

namespace VinylExchange.Services.HelperServices.Releases
{
    public interface IReleaseFilesService
    {
        Task<IEnumerable<ReleaseFile>> AddFilesForRelease(Guid releaseId, Guid formSessionId);
        Task<IEnumerable<ReleaseFileResourceModel>> GetReleaseTracks(Guid releaseId);
        Task<IEnumerable<ReleaseFileResourceModel>> GetReleaseImages(Guid releaseId);
        Task<ReleaseFileResourceModel> GetReleaseCoverArt(Guid releaseId);

    }
}
