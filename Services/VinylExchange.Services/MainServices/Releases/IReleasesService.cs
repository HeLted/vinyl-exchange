using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Models.ResourceModels.Releases;


namespace VinylExchange.Services.MainServices.Releases
{
    public interface IReleasesService
    {
        Task<IEnumerable<GetAllReleasesResourceModel>> GetReleases(string searchTerm, 
            IEnumerable<int> filterStyleIds,
            int releasesToSkip);
        Task<GetReleaseResourceModel> GetRelease(Guid releaseId);
        Task<Release> CreateRelease(CreateReleaseInputModel inputModel, Guid formSessionId);

    }
}
