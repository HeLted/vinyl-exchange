using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;
using Xunit;
using VinylExchange.Services.Mapping;
using VinylExchange.Web.Models.ResourceModels.Sales;

namespace VinylExchange.Services.Data.Tests
{
    public class MappingServicesTests
    {

        public MappingServicesTests()
        {

        }

        [Fact]
        public void ToShouldMapObject()
        {
            Assert.NotNull(new Sale().To<GetSaleResourceModel>());
        }

        [Fact]
        public void ToShouldThrowArgumentNullExceptionIfNullValueIsPassedToIt()
        {
            Assert.ThrowsAny<ArgumentNullException>(()=>((Sale)null).To<GetSaleResourceModel>());
        }

    }
}
