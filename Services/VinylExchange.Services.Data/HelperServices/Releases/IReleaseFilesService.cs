namespace VinylExchange.Services.Data.HelperServices.Releases
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Data.Models;

    public interface IReleaseFilesService
    {
        Task<List<ReleaseFile>> AddFilesForRelease(Guid? releaseId, Guid formSessionId);

        Task<TModel> GetReleaseCoverArt<TModel>(Guid? releaseId);

        Task<List<TModel>> GetReleaseImages<TModel>(Guid? releaseId);

        Task<List<TModel>> GetReleaseTracks<TModel>(Guid? releaseId);
    }
}