namespace VinylExchange.Services.Data.MainServices.Releases.Contracts
{
    using System;
    using System.Threading.Tasks;
    using VinylExchange.Data.Models;

    public interface IReleasesEntityRetriever
    {
        Task<Release> GetRelease(Guid? releaseId);
    }
}