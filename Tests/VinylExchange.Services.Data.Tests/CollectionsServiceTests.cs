using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data;
using VinylExchange.Services.Data.MainServices.Collections;
using VinylExchange.Services.Data.Tests.TestFactories;

namespace VinylExchange.Services.Data.Tests
{
    public class CollectionsServiceTests
    {
        private readonly ICollectionsService collectionsService;

        private readonly VinylExchangeDbContext dbContext;

        //private readonly CreateAddressInputModel testCreateAddressInputModel =
        //    new CreateAddressInputModel
        //        {
        //            Country = "Bulgaria", Town = "Sofia", PostalCode = "1612", FullAddress = "Test"
        //        };

        public CollectionsServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.collectionsService = new CollectionsService(this.dbContext);
        }

    }
}
