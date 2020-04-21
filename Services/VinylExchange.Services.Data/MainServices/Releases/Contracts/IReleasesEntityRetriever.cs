namespace VinylExchange.Services.Data.MainServices.Releases.Contracts
{
    #region

    using System;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;

    #endregion

    public interface IReleasesEntityRetriever
    {
        Task<Release> GetRelease(Guid? releaseId);
    }
}