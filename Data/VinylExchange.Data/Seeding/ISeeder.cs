namespace VinylExchange.Data.Seeding
{
    #region

    using System;
    using System.Threading.Tasks;

    #endregion

    public interface ISeeder
    {
        Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider);
    }
}