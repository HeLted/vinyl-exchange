using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Addresses;
using VinylExchange.Models.ResourceModels.Addresses;
using VinylExchange.Models.Utility;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.Data.MainServices.Addresses
{
    public class AddressesService : IAddressesService
    {
        private readonly VinylExchangeDbContext dbContext;

        public AddressesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //public async Task<GetCollectionItemResourceModel> GetCollectionItem(Guid collectionItemId)
        //   => await this.dbContext.Collections
        //       .Where(ci => ci.Id == collectionItemId)
        //       .To<GetCollectionItemResourceModel>()
        //       .FirstOrDefaultAsync();



        public async Task<Address> AddAddress(AddAdressInputModel inputModel,Guid userId)
        {

            var address = inputModel.To<Address>();

            address.UserId = userId;

            var trackedAddress = await this.dbContext.Addresses.AddAsync(address);

            await this.dbContext.SaveChangesAsync();

            return trackedAddress.Entity;

        }

        public async Task<GetAddressInfoUtilityModel> GetAddressInfo(Guid addressId)
            => await this.dbContext.Addresses
                .Where(a => a.Id == addressId)
                .To<GetAddressInfoUtilityModel>()
                .FirstOrDefaultAsync();


        public async Task<IEnumerable<GetUserAddressesResourceModel>> GetUserAddresses(Guid userId)
            => await this.dbContext.Addresses
                .Where(a => a.UserId == userId)
                .To<GetUserAddressesResourceModel>()
                .ToListAsync();


        public async Task<RemoveAddressResourceModel> RemoveAddress(Guid addressId)
        {
            var address = await this.dbContext.Addresses.FirstOrDefaultAsync(a=> a.Id == addressId);

            if (address == null)
            {
                throw new NullReferenceException("Address with this Id doesn't exist");
            }

            this.dbContext.Addresses.Remove(address);
            await this.dbContext.SaveChangesAsync();

            var resourceModel = address.To<RemoveAddressResourceModel>();

            return resourceModel;
        }
    }
}
