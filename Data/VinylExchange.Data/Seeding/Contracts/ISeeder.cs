namespace VinylExchange.Data.Seeding.Contracts
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