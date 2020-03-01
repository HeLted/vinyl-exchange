using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Addresses;
using VinylExchange.Models.ResourceModels.Addresses;
using VinylExchange.Models.Utility;

namespace VinylExchange.Services.Data.MainServices.Addresses
{
    public interface IAddressesService
    {
        Task<Address> AddAddress(AddAdressInputModel inputModel, Guid userId);

        Task<RemoveAddressResourceModel> RemoveAddress(Guid addressId);

        Task<GetAddressInfoUtilityModel> GetAddressInfo(Guid addressId);

        Task<IEnumerable<GetUserAddressesResourceModel>> GetUserAddresses(Guid userId);
    }
}
