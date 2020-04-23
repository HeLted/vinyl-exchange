namespace VinylExchange.Services.Data.MainServices.Addresses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using Contracts;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;

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

            var trackedAddress = await dbContext.Addresses.AddAsync(address);

            await dbContext.SaveChangesAsync();

            return trackedAddress.Entity.To<TModel>();
        }

        public async Task<TModel> GetAddress<TModel>(Guid addressId)
        {
            return await dbContext.Addresses.Where(a => a.Id == addressId).To<TModel>().FirstOrDefaultAsync();
        }

        public async Task<List<TModel>> GetUserAddresses<TModel>(Guid userId)
        {
            return await dbContext.Addresses.Where(a => a.UserId == userId).To<TModel>().ToListAsync();
        }

        public async Task<TModel> RemoveAddress<TModel>(Guid addressId)
        {
            var address = await dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);

            if (address == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.AddressNotFound);
            }

            var removedAddress = dbContext.Addresses.Remove(address).Entity;
            await dbContext.SaveChangesAsync();

            return removedAddress.To<TModel>();
        }

        public async Task<Address> GetAddress(Guid? addressId)
        {
            return await dbContext.Addresses.Where(a => a.Id == addressId).FirstOrDefaultAsync();
        }
    }
}