namespace VinylExchange.Services.Data.MainServices.Addresses
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;
    using VinylExchange.Web.Models.InputModels.Addresses;
    using VinylExchange.Web.Models.ResourceModels.Addresses;
    using VinylExchange.Web.Models.Utility;

    public interface IAddressesService
    {
        Task<TModel> AddAddress<TModel>(CreateAddressInputModel inputModel, Guid userId);

        Task<TModel> GetAddressInfo<TModel>(Guid addressId);

        Task<List<TModel>> GetUserAddresses<TModel>(Guid userId);

        Task<TModel> RemoveAddress<TModel>(Guid addressId);
    }
}