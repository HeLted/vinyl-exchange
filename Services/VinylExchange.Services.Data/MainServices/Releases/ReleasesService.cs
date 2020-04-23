namespace VinylExchange.Services.Data.MainServices.Releases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using HelperServices.Releases;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;

    public class ReleasesService : IReleasesService, IReleasesEntityRetriever
    {
        private const int ReleasesToTake = 8;

        private readonly VinylExchangeDbContext dbContext;

        private readonly IReleaseFilesService releaseFilesService;

        public ReleasesService(VinylExchangeDbContext dbContext, IReleaseFilesService releaseFilesService)
        {
            this.dbContext = dbContext;
            this.releaseFilesService = releaseFilesService;
        }

        public async Task<TModel> CreateRelease<TModel>(
            string artist,
            string title,
            string format,
            int year,
            string label,
            ICollection<int> styleIds,
            Guid formSessionId)
        {
            var release = new Release
            {
                Artist = artist,
                Title = title,
                Format = format,
                Year = year,
                Label = label
            };

            var trackedRelease = await dbContext.Releases.AddAsync(release);

            await releaseFilesService.AddFilesForRelease(release.Id, formSessionId);

            await AddStylesForRelease(release.Id, styleIds);

            await dbContext.SaveChangesAsync();

            return trackedRelease.Entity.To<TModel>();
        }

        public async Task<TModel> GetRelease<TModel>(Guid? releaseId)
        {
            return await dbContext.Releases.Where(x => x.Id == releaseId).To<TModel>().FirstOrDefaultAsync();
        }

        public async Task<List<TModel>> GetReleases<TModel>(
            string searchTerm,
            int? filterGenreId,
            IEnumerable<int> filterStyleIds,
            int releasesToSkip)
        {
            List<TModel> releases = null;

            var releasesQuariable = dbContext.Releases.AsQueryable();

            if (searchTerm != null)
            {
                releasesQuariable = releasesQuariable.Where(
                    r => r.Artist.ToLower().Contains(searchTerm.ToLower())
                         || r.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            if (filterGenreId != null)
            {
                if (filterStyleIds.Count() == 0)
                {
                    releasesQuariable =
                        releasesQuariable.Where(r => r.Styles.Any(s => s.Style.GenreId == filterGenreId));
                }
                else
                {
                    releasesQuariable = releasesQuariable.Where(
                        r => r.Styles.Any(
                            sr => filterStyleIds.Contains(sr.StyleId)
                                  && r.Styles.All(sr => sr.Style.GenreId == filterGenreId)));
                }
            }

            releases = await releasesQuariable.Skip(releasesToSkip).Take(ReleasesToTake).To<TModel>().ToListAsync();

            return releases;
        }

        public async Task<Release> GetRelease(Guid? releaseId)
        {
            return await dbContext.Releases.Where(x => x.Id == releaseId).FirstOrDefaultAsync();
        }

        private async Task AddStylesForRelease(Guid? releaseId, ICollection<int> styleIds)
        {
            foreach (var styleId in styleIds)
            {
                var styleRelease = new StyleRelease {ReleaseId = releaseId, StyleId = styleId};

                await dbContext.StyleReleases.AddAsync(styleRelease);
            }
        }
    }
}