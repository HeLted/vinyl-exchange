namespace VinylExchange.Services.Data.MainServices.Releases
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.InputModels.Releases;

    #endregion

    public interface IReleasesService
    {
        Task<TModel> CreateRelease<TModel>(CreateReleaseInputModel inputModel, Guid formSessionId);

        Task<TModel> GetRelease<TModel>(Guid? releaseId);

        Task<List<TModel>> GetReleases<TModel>(
            string searchTerm,
            int? filterGenreId,
            IEnumerable<int> filterStyleIds,
            int releasesToSkip);
    }
}