namespace VinylExchange.Services.MainServices.Releases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.HelperServices.Releases;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Releases;
    using VinylExchange.Web.Models.ResourceModels.Releases;

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

        public async Task<Release> CreateRelease(CreateReleaseInputModel inputModel, Guid formSessionId)
        {
            Release release = inputModel.To<Release>();

            EntityEntry<Release> trackedRelease = await this.dbContext.Releases.AddAsync(release);

            await this.releaseFilesService.AddFilesForRelease(release.Id, formSessionId);

            await this.AddStylesForRelease(release.Id, inputModel.StyleIds);

            await this.dbContext.SaveChangesAsync();

            return trackedRelease.Entity;
        }

        public async Task<GetReleaseResourceModel> GetRelease(Guid releaseId)
        {
            GetReleaseResourceModel release = await this.dbContext.Releases.To<GetReleaseResourceModel>()
                                                  .SingleOrDefaultAsync(x => x.Id == releaseId);

            release.CoverArt = await this.releaseFilesService.GetReleaseCoverArt(release.Id);

            return release;
        }

        public async Task<IEnumerable<GetReleasesResourceModel>> GetReleases(
            string searchTerm,
            IEnumerable<int> filterStyleIds,
            int releasesToSkip)
        {
            List<GetReleasesResourceModel> releases = null;

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
                           .To<GetReleasesResourceModel>().ToListAsync();

            releases.ForEach(
                r => { r.CoverArt = Task.Run(() => this.releaseFilesService.GetReleaseCoverArt(r.Id)).Result; });

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