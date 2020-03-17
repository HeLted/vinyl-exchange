using System;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Services.Data.MainServices.Addresses;
using VinylExchange.Services.Data.Tests.Fixtures;
using VinylExchange.Web.Models.Utility;
using Xunit;

namespace VinylExchange.Services.Data.Tests
{
    [Collection("AutoMapper")]
    public class AddressesServiceTests : IClassFixture<DatabaseFixture>
    {

        private readonly DatabaseFixture fixture;

        public AddressesServiceTests(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task TestTest()
        {
          
            var address = new Address()
            {
                Country = "Bulgaria",
                Town = "Sofia",
                PostalCode = "1612",
                FullAddress = "Test",
                UserId = Guid.NewGuid(),

            };

            fixture.dbContext.Addresses.Add(address);

            await fixture.dbContext.SaveChangesAsync();

            var addressService = new AddressesService(fixture.dbContext);

            var addressModel = await  addressService.GetAddressInfo<GetAddressInfoUtilityModel>(address.Id);

            Assert.Equal(address.UserId, addressModel.UserId);

        }

    }
}
