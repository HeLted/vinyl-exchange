using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Reflection;
using VinylExchange.Data;
using VinylExchange.Services.Mapping;
using VinylExchange.Web.Models;

namespace VinylExchange.Services.Data.Tests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<VinylExchangeDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            ModelBuilder modelBuilder = new ModelBuilder(new ConventionSet());

            var dbContext = new VinylExchangeDbContext(options, null);

            var onModelCreatingMethod = dbContext.GetType().GetMethod("OnModelCreating", BindingFlags.Instance | BindingFlags.NonPublic);

            onModelCreatingMethod.Invoke(dbContext, new object[] { modelBuilder });                      

            this.dbContext = dbContext;
        }

        public VinylExchangeDbContext dbContext { get; private set; }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }
           
    }
}
