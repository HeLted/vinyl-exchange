using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Reflection;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Services.Data.MainServices.Addresses;
using VinylExchange.Services.Mapping;
using VinylExchange.Web.Models;
using VinylExchange.Web.Models.Utility;
using Xunit;

namespace VinylExchange.Services.Data.Tests
{
    public class AddressesServiceTests
    {
        [Fact]
        public async Task TestTest()
        {
            var options = new DbContextOptionsBuilder<VinylExchangeDbContext>()
                .UseInMemoryDatabase("AddressesServiceTestDb").Options;

            ModelBuilder modelBuilder = new ModelBuilder(new ConventionSet());

            var dbContext = new VinylExchangeDbContext(options,null);

            var onModelCreatingMethod = dbContext.GetType().GetMethod("OnModelCreating", BindingFlags.Instance | BindingFlags.NonPublic);

            onModelCreatingMethod.Invoke(dbContext,new object [] { modelBuilder });

            AutoMapperConfig.RegisterMappings(typeof(ModelGetAssemblyClass).GetTypeInfo().Assembly);

            var address = new Address()
            {
                Country = "Bulgaria",
                Town = "Sofia",
                PostalCode = "1612",
                FullAddress = "Test",
                UserId = Guid.NewGuid(),

            };

            dbContext.Addresses.Add(address);

            await dbContext.SaveChangesAsync();

            var addressService = new AddressesService(dbContext);

            var addressModel = await  addressService.GetAddressInfo<GetAddressInfoUtilityModel>(address.Id);

            Assert.Equal(address.UserId, addressModel.UserId);

        }

    }
}
