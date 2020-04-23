namespace VinylExchange.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Models;

    public class StyleReleasesSeeder : ISeeder
    {
        public async Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.StyleReleases.Any())
            {
                await SeedStyleRelease(dbContext, Guid.Parse("3cc8ee5c-4dc3-4009-b3fc-d768e6a564f8"), 7);
                await SeedStyleRelease(dbContext, Guid.Parse("3cc8ee5c-4dc3-4009-b3fc-d768e6a564f8"), 10);

                await SeedStyleRelease(dbContext, Guid.Parse("1cf99ed0-a565-4d2a-928b-99a0fd851d9b"), 3);

                await SeedStyleRelease(dbContext, Guid.Parse("22d663f8-6bd6-4f20-9f74-bd82da066e42"), 7);
                await SeedStyleRelease(dbContext, Guid.Parse("22d663f8-6bd6-4f20-9f74-bd82da066e42"), 8);

                await SeedStyleRelease(dbContext, Guid.Parse("eb9101dc-d7d4-4558-8211-cdd9fd9d60f9"), 12);
                await SeedStyleRelease(dbContext, Guid.Parse("eb9101dc-d7d4-4558-8211-cdd9fd9d60f9"), 13);
                await SeedStyleRelease(dbContext, Guid.Parse("eb9101dc-d7d4-4558-8211-cdd9fd9d60f9"), 14);

                await SeedStyleRelease(dbContext, Guid.Parse("4b3b4142-3621-4256-a209-4468a6c5ca4c"), 1);

                await SeedStyleRelease(dbContext, Guid.Parse("6eaabd12-fd4f-4b69-b0bd-f172f9b42085"), 15);
            }
        }

        private static async Task SeedStyleRelease(VinylExchangeDbContext dbContext, Guid releaseId, int styleId)
        {
            var styleRelease = new StyleRelease {ReleaseId = releaseId, StyleId = styleId};

            await dbContext.StyleReleases.AddAsync(styleRelease);
        }
    }
}