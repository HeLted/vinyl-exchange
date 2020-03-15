﻿namespace VinylExchange.Services.Data.MainServices.Addresses
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Web.Models.InputModels.Addresses;

    public interface IAddressesService
    {
        Task<TModel> AddAddress<TModel>(CreateAddressInputModel inputModel, Guid userId);

        Task<TModel> GetAddressInfo<TModel>(Guid addressId);

        Task<List<TModel>> GetUserAddresses<TModel>(Guid userId);

        Task<TModel> RemoveAddress<TModel>(Guid addressId);
    }
}