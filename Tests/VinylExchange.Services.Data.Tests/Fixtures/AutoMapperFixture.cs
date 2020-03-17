namespace VinylExchange.Services.Data.Tests.Fixtures
{
    #region

    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models;

    using Xunit;

    #endregion

    public class AutoMapperFixture
    {
        public AutoMapperFixture()
        {
            this.ConfigureAutoMapper();
        }

        private void ConfigureAutoMapper() =>
            AutoMapperConfig.RegisterMappings(typeof(ModelGetAssemblyClass).GetTypeInfo().Assembly);
    }

    [SuppressMessage(
        "StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = @"This class has no code, and is never created.Its purpose is simply
        to be the place to apply[CollectionDefinition] and all the  ICollectionFixture <> interfaces.")]
    [CollectionDefinition("AutoMapper")]
    public class AutoMapperCollection : ICollectionFixture<AutoMapperFixture>
    {
    }
}