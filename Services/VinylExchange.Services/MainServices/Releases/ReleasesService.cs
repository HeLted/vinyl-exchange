namespace VinylExchange.Services.MainServices.Releases
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.HelperServices.Releases;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Releases;

    public class ReleasesService : IReleasesService
    {
        private const int ReleasesToTake = 6;

        private readonly VinylExchangeDbContext dbContext;

        private readonly IReleaseFilesService releaseFilesService;

        public ReleasesService(VinylExchangeDbContext dbContext, IReleaseFilesService releaseFilesService)
        {
            this.dbContext = dbContext;
            this.releaseFilesService = releaseFilesService;
        }

        public async Task<TModel> CreateRelease<TModel>(CreateReleaseInputModel inputModel, Guid formSessionId)
        {
            Release release = inputModel.To<Release>();

            EntityEntry<Release> trackedRelease = await this.dbContext.Releases.AddAsync(release);

            await this.releaseFilesService.AddFilesForRelease(release.Id, formSessionId);

            await this.AddStylesForRelease(release.Id, inputModel.StyleIds);

            await this.dbContext.SaveChangesAsync();

            return trackedRelease.Entity.To<TModel>();
        }

        public async Task<TModel> GetRelease<TModel>(Guid releaseId)
            => await this.dbContext.Releases.Where(x => x.Id == releaseId)
                                                  .To<TModel>()
                                                  .FirstOrDefaultAsync();
        
       

        public async Task<List<TModel>> GetReleases<TModel>(
            string searchTerm,
            IEnumerable<int> filterStyleIds,
            int releasesToSkip)
        {
            List<TModel> releases = null;

            IQueryable<Release> releasesQuariable = this.dbContext.Releases.AsQueryable();

            if (searchTerm != null)
            {
                releasesQuariable =
                    releasesQuariable.Where(r => r.Artist.Contains(searchTerm) || r.Title.Contains(searchTerm));
            }

            releases = await releasesQuariable
                           .Where(
                               r => r.Styles.Any(s => filterStyleIds.Contains(s.StyleId))
                                    || filterStyleIds.Count() == 0).Skip(releasesToSkip).Take(ReleasesToTake)
                           .To<TModel>().ToListAsync();
                     

            return releases;
        }

        private async Task AddStylesForRelease(Guid releaseId, ICollection<int> styleIds)
        {
            foreach (int styleId in styleIds)
            {
                StyleRelease styleRelease = new StyleRelease() { ReleaseId = releaseId, StyleId = styleId };

                await this.dbContext.StyleReleases.AddAsync(styleRelease);
            }
        }
    }
}