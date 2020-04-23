namespace VinylExchange.Services.Data.MainServices.Addresses.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAddressesService
    {
        Task<TModel> CreateAddress<TModel>(
            string country,
            string town,
            string postalCode,
            string fullAddress,
            Guid userId);

        Task<TModel> GetAddress<TModel>(Guid addressId);

        Task<List<TModel>> GetUserAddresses<TModel>(Guid userId);

        Task<TModel> RemoveAddress<TModel>(Guid addressId);
    }
}