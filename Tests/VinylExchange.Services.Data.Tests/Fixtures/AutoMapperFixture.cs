using System;
using System.Reflection;
using VinylExchange.Services.Mapping;
using VinylExchange.Web.Models;
using Xunit;

namespace VinylExchange.Services.Data.Tests.Fixtures
{
    public class AutoMapperFixture 
    {
        public AutoMapperFixture()
        {
            this.ConfigureAutoMapper();
        }          

        private void ConfigureAutoMapper() => AutoMapperConfig
            .RegisterMappings(typeof(ModelGetAssemblyClass)
            .GetTypeInfo().Assembly);
    }

    [CollectionDefinition("AutoMapper")]
    public class DatabaseCollection : ICollectionFixture<AutoMapperFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

}
