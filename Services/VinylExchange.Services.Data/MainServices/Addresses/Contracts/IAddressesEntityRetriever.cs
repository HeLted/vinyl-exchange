namespace VinylExchange.Services.Data.MainServices.Addresses.Contracts
{
    using System;
    using System.Threading.Tasks;
    using VinylExchange.Data.Models;

    public interface IAddressesEntityRetriever
    {
        Task<Address> GetAddress(Guid? addressId);
    }
}