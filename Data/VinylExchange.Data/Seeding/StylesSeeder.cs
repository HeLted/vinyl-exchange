namespace VinylExchange.Data.Seeding
{
    #region

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using VinylExchange.Data.Models;
    using VinylExchange.Data.Seeding.Contracts;

    #endregion

    internal class StylesSeeder : ISeeder
    {
        public async Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedStyleAsync(dbContext, "Trance", 1);
            await SeedStyleAsync(dbContext, "House", 1);
            await SeedStyleAsync(dbContext, "Techno", 1);
            await SeedStyleAsync(dbContext, "IDM", 1);
            await SeedStyleAsync(dbContext, "Drum And Bass", 1);
            await SeedStyleAsync(dbContext, "Hardcore", 1);
            await SeedStyleAsync(dbContext, "Downtempo", 1);
            await SeedStyleAsync(dbContext, "Breakbeat", 1);
            await SeedStyleAsync(dbContext, "Big Beat", 1);
            await SeedStyleAsync(dbContext, "Ambient", 1);
            await SeedStyleAsync(dbContext, "Leftfield", 1);

            await SeedStyleAsync(dbContext, "Metal", 2);
            await SeedStyleAsync(dbContext, "Trash Metal", 2);
            await SeedStyleAsync(dbContext, "Alternative Rock", 2);

            await SeedStyleAsync(dbContext, "Rap", 3);

            await SeedStyleAsync(dbContext, "Easy Listening", 4);

            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedStyleAsync(VinylExchangeDbContext dbContext, string name, int genreId)
        {
            if (!dbContext.Styles.Any())
            {
                var style = new Style { Name = name, GenreId = genreId };

                await dbContext.Styles.AddAsync(style);
            }
        }
    }
}