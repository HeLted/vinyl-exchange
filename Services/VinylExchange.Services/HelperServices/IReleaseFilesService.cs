using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.Utility;
using VinylExchange.Models.ViewModels.ReleaseFiles;

namespace VinylExchange.Services.HelperServices
{
    public interface IReleaseFilesService
    {
        Task<IEnumerable<ReleaseFile>> AddFilesForRelease(Guid releaseId, Guid formSessionId);
        Task<IEnumerable<ReleaseFileViewModel>> GetReleaseTracks(Guid releaseId);
        Task<IEnumerable<ReleaseFileViewModel>> GetReleaseImages(Guid releaseId);
        Task<ReleaseFileViewModel> GetReleaseCoverArt(Guid releaseId);

    }
}
