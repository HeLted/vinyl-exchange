namespace VinylExchange.Services.Data.MainServices.Addresses.Contracts
{
    #region

    using System;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;

    #endregion

    public interface IAddressesEntityRetriever
    {
        Task<Address> GetAddress(Guid? addressId);
    }
}