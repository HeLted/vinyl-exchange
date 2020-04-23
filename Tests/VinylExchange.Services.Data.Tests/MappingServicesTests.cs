namespace VinylExchange.Services.Data.Tests
{
    using System;
    using Mapping;
    using VinylExchange.Data.Models;
    using Web.Models.ResourceModels.Sales;
    using Xunit;

    public class MappingServicesTests
    {
        [Fact]
        public void ToShouldMapObject()
        {
            Assert.NotNull(new Sale().To<GetSaleResourceModel>());
        }

        [Fact]
        public void ToShouldThrowArgumentNullExceptionIfNullValueIsPassedToIt()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => ((Sale) null).To<GetSaleResourceModel>());
        }
    }
}