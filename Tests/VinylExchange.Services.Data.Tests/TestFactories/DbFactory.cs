using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Reflection;
using VinylExchange.Data;

namespace VinylExchange.Services.Data.Tests.TestFactories
{
    public  static class DbFactory 
    {
        public static VinylExchangeDbContext CreateVinylExchangeDbContext()
        {
            var options = new DbContextOptionsBuilder<VinylExchangeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var modelBuilder = new ModelBuilder(new ConventionSet());

            var dbContext = new VinylExchangeDbContext(options, null);

            var onModelCreatingMethod = dbContext.GetType().GetMethod(
                "OnModelCreating",
                BindingFlags.Instance | BindingFlags.NonPublic);

            onModelCreatingMethod.Invoke(dbContext, new object[] { modelBuilder });

            return dbContext;
        }
        
    }
}
