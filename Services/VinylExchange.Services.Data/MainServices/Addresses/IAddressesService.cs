namespace VinylExchange.Services.Data.MainServices.Addresses
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.InputModels.Addresses;

    #endregion

    public interface IAddressesService
    {
        Task<TModel> CreateAddress<TModel>(CreateAddressInputModel inputModel, Guid userId);

        Task<TModel> GetAddressInfo<TModel>(Guid addressId);

        Task<List<TModel>> GetUserAddresses<TModel>(Guid userId);

        Task<TModel> RemoveAddress<TModel>(Guid addressId);
    }
}