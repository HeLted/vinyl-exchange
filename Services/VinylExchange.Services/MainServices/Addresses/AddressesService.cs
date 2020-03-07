namespace VinylExchange.Services.Data.MainServices.Addresses
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Addresses;
    using VinylExchange.Web.Models.ResourceModels.Addresses;
    using VinylExchange.Web.Models.Utility;

    public class AddressesService : IAddressesService
    {
        private readonly VinylExchangeDbContext dbContext;

        public AddressesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Address> AddAddress(AddAdressInputModel inputModel, Guid userId)
        {
            Address address = inputModel.To<Address>();

            address.UserId = userId;

            EntityEntry<Address> trackedAddress = await this.dbContext.Addresses.AddAsync(address);

            await this.dbContext.SaveChangesAsync();

            return trackedAddress.Entity;
        }

        public async Task<GetAddressInfoUtilityModel> GetAddressInfo(Guid addressId)
        {
            return await this.dbContext.Addresses.Where(a => a.Id == addressId).To<GetAddressInfoUtilityModel>()
                       .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetUserAddressesResourceModel>> GetUserAddresses(Guid userId)
        {
            return await this.dbContext.Addresses.Where(a => a.UserId == userId).To<GetUserAddressesResourceModel>()
                       .ToListAsync();
        }

        public async Task<RemoveAddressResourceModel> RemoveAddress(Guid addressId)
        {
            Address address = await this.dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);

            if (address == null)
            {
                throw new NullReferenceException("Address with this Id doesn't exist");
            }

            this.dbContext.Addresses.Remove(address);
            await this.dbContext.SaveChangesAsync();

            RemoveAddressResourceModel resourceModel = address.To<RemoveAddressResourceModel>();

            return resourceModel;
        }
    }
}