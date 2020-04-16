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
            if (!dbContext.Styles.Any())
            {
                await SeedStyleAsync(dbContext, "IDM", 1); // 1
                await SeedStyleAsync(dbContext, "Techno", 1); // 2
                await SeedStyleAsync(dbContext, "House", 1); // 3
                await SeedStyleAsync(dbContext, "Trance", 1); // 4
                await SeedStyleAsync(dbContext, "Drum And Bass", 1); // 5
                await SeedStyleAsync(dbContext, "Hardcore", 1); // 6
                await SeedStyleAsync(dbContext, "Downtempo", 1); // 7
                await SeedStyleAsync(dbContext, "Breakbeat", 1); // 8
                await SeedStyleAsync(dbContext, "Big Beat", 1); // 9
                await SeedStyleAsync(dbContext, "Ambient", 1); // 10
                await SeedStyleAsync(dbContext, "Leftfield", 1); // 11
                await SeedStyleAsync(dbContext, "Abstract", 1); // 12
                await SeedStyleAsync(dbContext, "Electro", 1); // 13
                await SeedStyleAsync(dbContext, "Experimental", 1); // 14

                await SeedStyleAsync(dbContext, "Metal", 2); // 12
                await SeedStyleAsync(dbContext, "Trash Metal", 2); // 13
                await SeedStyleAsync(dbContext, "Alternative Rock", 2); // 14

                await SeedStyleAsync(dbContext, "Rap", 3); // 15

                await SeedStyleAsync(dbContext, "Easy Listening", 4); // 16
            }
        }

        private static async Task SeedStyleAsync(VinylExchangeDbContext dbContext, string name, int genreId)
        {
            var style = new Style { Name = name, GenreId = genreId };

            await dbContext.Styles.AddAsync(style);

            await dbContext.SaveChangesAsync();
        }
    }
}