namespace VinylExchange.Data.Seeding
{
    #region

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;
    using VinylExchange.Data.Seeding.Contracts;

    #endregion

    internal class GenresSeeder : ISeeder
    {
        public async Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedGenreAsync(dbContext, "Electronic");

            await SeedGenreAsync(dbContext, "Rock");

            await SeedGenreAsync(dbContext, "Hip Hop");

            await SeedGenreAsync(dbContext, "Jazz");

            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedGenreAsync(VinylExchangeDbContext dbContext, string name)
        {
            if (!dbContext.Genres.Any())
            {
                var genre = new Genre { Name = name };

                await dbContext.Genres.AddAsync(genre);
            }
        }
    }
}