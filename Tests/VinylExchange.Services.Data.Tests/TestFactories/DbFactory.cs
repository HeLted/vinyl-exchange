namespace VinylExchange.Services.Data.Tests.TestFactories
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using VinylExchange.Data;

    internal static class DbFactory
    {
        public static VinylExchangeDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<VinylExchangeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var modelBuilder = new ModelBuilder(new ConventionSet());

            var dbContext = new VinylExchangeDbContext(options, null);

            var onModelCreatingMethod = dbContext.GetType().GetMethod(
                "OnModelCreating",
                BindingFlags.Instance | BindingFlags.NonPublic);

            onModelCreatingMethod.Invoke(dbContext, new object[] {modelBuilder});

            return dbContext;
        }
    }
}