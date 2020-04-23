namespace VinylExchange.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Models;

    internal class GenresSeeder : ISeeder
    {
        public async Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Genres.Any())
            {
                await SeedGenreAsync(dbContext, "Electronic");

                await SeedGenreAsync(dbContext, "Rock");

                await SeedGenreAsync(dbContext, "Hip Hop");

                await SeedGenreAsync(dbContext, "Jazz");

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedGenreAsync(VinylExchangeDbContext dbContext, string name)
        {
            var genre = new Genre {Name = name};

            await dbContext.Genres.AddAsync(genre);
        }
    }
}