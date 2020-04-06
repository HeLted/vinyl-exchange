namespace VinylExchange.Services.Data.MainServices.Releases
{
    #region

    using System;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;

    #endregion

    public interface IReleasesEntityRetriever
    {
        Task<Release> GetRelease(Guid? addressId);
    }
}