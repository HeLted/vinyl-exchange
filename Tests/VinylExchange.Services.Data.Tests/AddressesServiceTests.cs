namespace VinylExchange.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using MainServices.Addresses;
    using Microsoft.EntityFrameworkCore;
    using TestFactories;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using Web.Models.ResourceModels.Addresses;
    using Web.Models.Utility.Addresses;
    using Xunit;

    public class AddressesServiceTests
    {
        private readonly AddressesService addressesService;

        private readonly VinylExchangeDbContext dbContext;

        public AddressesServiceTests()
        {
            dbContext = DbFactory.CreateDbContext();

            addressesService = new AddressesService(dbContext);
        }

        [Fact]
        public async Task CreateAddressShouldCreateAddress()
        {
            var createdAddressModel = await addressesService.CreateAddress<CreateAddressResourceModel>(
                "Bulgaria",
                "Sofia",
                "1612",
                "Test",
                Guid.NewGuid());

            await dbContext.SaveChangesAsync();

            var createdAddress =
                await dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == createdAddressModel.Id);

            Assert.NotNull(createdAddress);
        }

        [Fact]
        public async Task CreateAddressShouldCreateAddressWithCorrectData()
        {
            var createdAddressModel = await addressesService.CreateAddress<CreateAddressResourceModel>(
                "Bulgaria",
                "Sofia",
                "1612",
                "Test",
                Guid.NewGuid());

            await dbContext.SaveChangesAsync();

            var createdAddress =
                await dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == createdAddressModel.Id);

            Assert.Equal("Bulgaria", createdAddress.Country);
            Assert.Equal("Sofia", createdAddress.Town);
            Assert.Equal("1612", createdAddress.PostalCode);
            Assert.Equal("Test", createdAddress.FullAddress);
        }

        [Fact]
        public async Task GetAddressShouldReturnCorrectAddress()
        {
            var address = (await dbContext.Addresses.AddAsync(new Address())).Entity;

            await dbContext.SaveChangesAsync();

            var returnedAddressModel = await addressesService.GetAddress<GetAddressInfoUtilityModel>(address.Id);

            Assert.Equal(address.Id, returnedAddressModel.Id);
        }

        [Fact]
        public async Task GetAddressShouldReturnNullIfProvidedAddressIdIsNotExistingInDb()
        {
            await dbContext.Addresses.AddAsync(new Address());

            await dbContext.SaveChangesAsync();

            var returnedAddressModel =
                await addressesService.GetAddress<GetAddressInfoUtilityModel>(Guid.NewGuid());

            Assert.Null(returnedAddressModel);
        }

        [Fact]
        public async Task GetUserAddressesShouldReturnEmptyListWhenANonExistingUserIdIsProvided()
        {
            var userId = Guid.NewGuid();

            for (var i = 0; i < 3; i++)
            {
                await dbContext.Addresses.AddAsync(new Address {UserId = userId});
            }

            await dbContext.SaveChangesAsync();

            var userAddressesModels =
                await addressesService.GetUserAddresses<GetAddressInfoUtilityModel>(Guid.NewGuid());

            Assert.True(userAddressesModels.Count == 0);
        }

        [Fact]
        public async Task GetUserAddressesShouldReturnUserAddresses()
        {
            var userId = Guid.NewGuid();

            for (var i = 0; i < 3; i++)
            {
                await dbContext.Addresses.AddAsync(new Address {UserId = userId});
            }

            await dbContext.SaveChangesAsync();

            var userAddressesModels = await addressesService.GetUserAddresses<GetAddressInfoUtilityModel>(userId);

            Assert.True(userAddressesModels.All(a => a.UserId == userId));
        }

        [Fact]
        public async Task RemoveAddressShouldRemoveAddress()
        {
            var address = (await dbContext.Addresses.AddAsync(new Address())).Entity;

            await dbContext.SaveChangesAsync();

            await addressesService.RemoveAddress<RemoveAddressResourceModel>(address.Id);

            var removedAddress = await dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == address.Id);

            Assert.Null(removedAddress);
        }

        [Fact]
        public async Task RemoveAddressShouldThrowNullReferenceExceptionIfProvidedAddressIdDoesntBelongToAnyAddress()
        {
            await dbContext.Addresses.AddAsync(new Address());

            await dbContext.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () =>
                    await addressesService.RemoveAddress<RemoveAddressResourceModel>(
                        Guid.NewGuid()));

            Assert.Equal(NullReferenceExceptionsConstants.AddressNotFound, exception.Message);
        }

        [Fact]
        public async Task GetAddressShouldGetAddress()
        {
            var address = new Address();

            await dbContext.Addresses.AddAsync(address);

            await dbContext.SaveChangesAsync();

            var returnedAddress = await addressesService.GetAddress(address.Id);

            Assert.NotNull(returnedAddress);
        }
    }
}