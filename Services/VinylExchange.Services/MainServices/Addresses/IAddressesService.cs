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
        Task<Address> AddAddress(AddAdressInputModel inputModel, Guid userId);

        Task<GetAddressInfoUtilityModel> GetAddressInfo(Guid addressId);

        Task<IEnumerable<GetUserAddressesResourceModel>> GetUserAddresses(Guid userId);

        Task<RemoveAddressResourceModel> RemoveAddress(Guid addressId);
    }
}