namespace VinylExchange.Services.Data.Tests.Fixtures
{
    #region

    using System;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;

    using VinylExchange.Data;

    #endregion

    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<VinylExchangeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var modelBuilder = new ModelBuilder(new ConventionSet());

            var dbContext = new VinylExchangeDbContext(options, null);

            var onModelCreatingMethod = dbContext.GetType().GetMethod(
                "OnModelCreating",
                BindingFlags.Instance | BindingFlags.NonPublic);

            onModelCreatingMethod.Invoke(dbContext, new object[] { modelBuilder });

            this.DbContext = dbContext;
        }

        public VinylExchangeDbContext DbContext { get; }

        public void Dispose()
        {
            this.DbContext.Dispose();
        }
    }
}