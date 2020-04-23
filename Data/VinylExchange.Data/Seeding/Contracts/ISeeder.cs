namespace VinylExchange.Data.Seeding.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider);
    }
}