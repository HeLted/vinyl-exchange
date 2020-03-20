namespace VinylExchange.Services.Data.MainServices.Addresses
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Common.Constants;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Addresses;

    #endregion

    public class AddressesService : IAddressesService
    {
        private readonly VinylExchangeDbContext dbContext;

        public AddressesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TModel> CreateAddress<TModel>(CreateAddressInputModel inputModel, Guid userId)
        {
            var address = inputModel.To<Address>();

            address.UserId = userId;

            var trackedAddress = await this.dbContext.Addresses.AddAsync(address);

            await this.dbContext.SaveChangesAsync();

            return trackedAddress.Entity.To<TModel>();
        }

        public async Task<TModel> GetAddressInfo<TModel>(Guid addressId)
        {
            return await this.dbContext.Addresses.Where(a => a.Id == addressId).To<TModel>().FirstOrDefaultAsync();
        }

        public async Task<List<TModel>> GetUserAddresses<TModel>(Guid userId)
        {
            return await this.dbContext.Addresses.Where(a => a.UserId == userId).To<TModel>().ToListAsync();
        }

        public async Task<TModel> RemoveAddress<TModel>(Guid addressId)
        {
            var address = await this.dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);

            if (address == null) throw new NullReferenceException(NullReferenceExceptionsConstants.AddressNotFound);

            var removedAddress = this.dbContext.Addresses.Remove(address).Entity;
            await this.dbContext.SaveChangesAsync();

            return removedAddress.To<TModel>();
        }
    }
}