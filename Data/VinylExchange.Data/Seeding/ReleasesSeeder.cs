using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;
using VinylExchange.Data.Seeding.Contracts;

namespace VinylExchange.Data.Seeding
{
    public class ReleasesSeeder : ISeeder
    {
        public async Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Releases.Any())
            {
                           await SeedReleaseAsync(dbContext,Guid.Parse("3cc8ee5c-4dc3-4009-b3fc-d768e6a564f8"),"Aphex Twin","Selected Ambient Works 85-92","4x Lp",1992,"Warp");
            }

        }

        private static async Task SeedReleaseAsync(VinylExchangeDbContext dbContext, Guid id, string artist, string title, string format, int year, string label)
        {
            var release = new Release
            {
                Id = id,
                Artist = artist,
                Title = title,
                Format = format,
                Year = year,
                Label = label
            };

            await dbContext.Releases.AddAsync(release);

            await dbContext.SaveChangesAsync();
        }
    }
}
