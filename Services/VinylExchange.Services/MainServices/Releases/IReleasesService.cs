namespace VinylExchange.Services.MainServices.Releases
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Web.Models.InputModels.Releases;

    public interface IReleasesService
    {
        Task<TModel> CreateRelease<TModel>(CreateReleaseInputModel inputModel, Guid formSessionId);

        Task<TModel> GetRelease<TModel>(Guid releaseId);

        Task<List<TModel>> GetReleases<TModel>(
            string searchTerm,
            int? filterGenreId,
            IEnumerable<int> filterStyleIds,
            int releasesToSkip);
    }
}