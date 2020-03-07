namespace VinylExchange.Services.MainServices.Releases
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;
    using VinylExchange.Web.Models.InputModels.Releases;
    using VinylExchange.Web.Models.ResourceModels.Releases;

    public interface IReleasesService
    {
        Task<Release> CreateRelease(CreateReleaseInputModel inputModel, Guid formSessionId);

        Task<GetReleaseResourceModel> GetRelease(Guid releaseId);

        Task<IEnumerable<GetReleasesResourceModel>> GetReleases(
            string searchTerm,
            IEnumerable<int> filterStyleIds,
            int releasesToSkip);
    }
}