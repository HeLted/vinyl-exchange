namespace VinylExchange.Services.Data.Tests
{
    #region

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Common.Constants;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Addresses;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Web.Models.InputModels.Addresses;
    using VinylExchange.Web.Models.ResourceModels.Addresses;
    using VinylExchange.Web.Models.Utility;

    using Xunit;

    #endregion

    [Collection("AutoMapperSetup")]
    public class AddressesServiceTests
    {
        private readonly IAddressesService addressesService;

        private readonly VinylExchangeDbContext dbContext;

        private readonly CreateAddressInputModel testCreateAddressInputModel =
            new CreateAddressInputModel
                {
                    Country = "Bulgaria", Town = "Sofia", PostalCode = "1612", FullAddress = "Test"
                };

        public AddressesServiceTests()
        {
            this.dbContext = DbFactory.CreateVinylExchangeDbContext();
            this.addressesService = new AddressesService(this.dbContext);
        }

        [Fact]
        public async Task CreateAddressShouldCreateAddress()
        {
            var createdAddressModel = await this.addressesService.CreateAddress<CreateAddressResourceModel>(
                                          this.testCreateAddressInputModel,
                                          Guid.NewGuid());

            await this.dbContext.SaveChangesAsync();

            var createdAddress =
                await this.dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == createdAddressModel.Id);

            Assert.NotNull(createdAddress);
        }

        [Fact]
        public async Task CreateAddressShouldCreateAddressWithCorrectData()
        {
            var createdAddressModel = await this.addressesService.CreateAddress<CreateAddressResourceModel>(
                                          this.testCreateAddressInputModel,
                                          Guid.NewGuid());

            await this.dbContext.SaveChangesAsync();

            var createdAddress =
                await this.dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == createdAddressModel.Id);

            Assert.Equal(this.testCreateAddressInputModel.Country, createdAddress.Country);
            Assert.Equal(this.testCreateAddressInputModel.Town, createdAddress.Town);
            Assert.Equal(this.testCreateAddressInputModel.PostalCode, createdAddress.PostalCode);
            Assert.Equal(this.testCreateAddressInputModel.FullAddress, createdAddress.FullAddress);
        }

        [Fact]
        public async Task GetAddressInfoShouldReturnCorrectAddress()
        {
            var address = (await this.dbContext.Addresses.AddAsync(new Address())).Entity;

            await this.dbContext.SaveChangesAsync();

            var returnedAddressModel =
                await this.addressesService.GetAddressInfo<GetAddressInfoUtilityModel>(address.Id);

            Assert.Equal(address.Id, returnedAddressModel.Id);
        }

        [Fact]
        public async Task GetAddressInfoShouldReturnNullIfProvidedAddressIdIsNotExistingInDb()
        {
            var address = (await this.dbContext.Addresses.AddAsync(new Address())).Entity;

            await this.dbContext.SaveChangesAsync();

            var returnedAddressModel =
                await this.addressesService.GetAddressInfo<GetAddressInfoUtilityModel>(Guid.NewGuid());

            Assert.Null(returnedAddressModel);
        }

        [Fact]
        public async Task GetUserAddressesShouldReturnEmptyListWhenANonExistingUserIdIsProvided()
        {
            var userId = Guid.NewGuid();

            for (var i = 0; i < 3; i++) await this.dbContext.Addresses.AddAsync(new Address { UserId = userId });

            await this.dbContext.SaveChangesAsync();

            var userAddressesModels =
                await this.addressesService.GetUserAddresses<GetAddressInfoUtilityModel>(Guid.NewGuid());

            Assert.True(userAddressesModels.Count == 0);
        }

        [Fact]
        public async Task GetUserAddressesShouldReturnUserAddresses()
        {
            var userId = Guid.NewGuid();

            for (var i = 0; i < 3; i++) await this.dbContext.Addresses.AddAsync(new Address { UserId = userId });

            await this.dbContext.SaveChangesAsync();

            var userAddressesModels = await this.addressesService.GetUserAddresses<GetAddressInfoUtilityModel>(userId);

            Assert.True(userAddressesModels.All(a => a.UserId == userId));
        }

        [Fact]
        public async Task RemoveAddressShouldRemoveAddress()
        {
            var address = (await this.dbContext.Addresses.AddAsync(new Address())).Entity;

            await this.dbContext.SaveChangesAsync();

            await this.addressesService.RemoveAddress<RemoveAddressResourceModel>(address.Id);

            var removedAddress = await this.dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == address.Id);

            Assert.Null(removedAddress);
        }

        [Fact]
        public async Task RemoveAddressShouldThrowNullReferenceExceptionIfProvidedAddressIdDoesntBelongToAnyAddress()
        {
            await this.dbContext.Addresses.AddAsync(new Address());

            await this.dbContext.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () =>
                                    await this.addressesService.RemoveAddress<RemoveAddressResourceModel>(
                                        Guid.NewGuid()));

            Assert.Equal(NullReferenceExceptionsConstants.AddressNotFound, exception.Message);
        }
    }
}