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
    using VinylExchange.Services.Data.MainServices.Addresses.Contracts;
    using VinylExchange.Services.Mapping;

    #endregion

    public class AddressesService : IAddressesService, IAddressesEntityRetriever
    {
        private readonly VinylExchangeDbContext dbContext;

        public AddressesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TModel> CreateAddress<TModel>(
            string country,
            string town,
            string postalCode,
            string fullAddress,
            Guid userId)
        {
            var address = new Address
                {
                    Country = country,
                    Town = town,
                    PostalCode = postalCode,
                    FullAddress = fullAddress,
                    UserId = userId
                };

            var trackedAddress = await this.dbContext.Addresses.AddAsync(address);

            await this.dbContext.SaveChangesAsync();

            return trackedAddress.Entity.To<TModel>();
        }

        public async Task<TModel> GetAddress<TModel>(Guid addressId)
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

            if (address == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.AddressNotFound);
            }

            var removedAddress = this.dbContext.Addresses.Remove(address).Entity;
            await this.dbContext.SaveChangesAsync();

            return removedAddress.To<TModel>();
        }

        public async Task<Address> GetAddress(Guid? addressId)
        {
            return await this.dbContext.Addresses.Where(a => a.Id == addressId).FirstOrDefaultAsync();
        }
    }
}