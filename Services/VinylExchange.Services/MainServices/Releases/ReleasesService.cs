using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Models.ResourceModels.Releases;
using VinylExchange.Services.HelperServices;
using VinylExchange.Services.HelperServices.Releases;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.MainServices.Releases
{
    public class ReleasesService : IReleasesService
    {
        private const int releasesToTake = 6;

        private readonly VinylExchangeDbContext dbContext;
        private readonly IReleaseFilesService releaseFilesService;

        public ReleasesService(VinylExchangeDbContext dbContext, IReleaseFilesService releaseFilesService)
        {
            this.dbContext = dbContext;
            this.releaseFilesService = releaseFilesService;
        }

        public async Task<IEnumerable<GetReleasesResourceModel>> GetReleases(string searchTerm, IEnumerable<int> filterStyleIds, int releasesToSkip)
        {

            List<GetReleasesResourceModel> releases = null;

            var releasesQuariable = dbContext.Releases.AsQueryable();

            if (searchTerm != null)
            {
                releasesQuariable = releasesQuariable.Where(r => r.Artist.Contains(searchTerm) || r.Title.Contains(searchTerm));
            }
            
            releases = await releasesQuariable
            .Where(r => r.Styles.Any(s => filterStyleIds.Contains(s.StyleId)) || filterStyleIds.Count() == 0)
            .Skip(releasesToSkip)
            .Take(releasesToTake)
            .To<GetReleasesResourceModel>()
            .ToListAsync();
                                   
            releases.ForEach(r =>
            {
                r.CoverArt = Task.Run(() => releaseFilesService.GetReleaseCoverArt(r.Id)).Result;
            });

            return releases;
        }


        public async Task<GetReleaseResourceModel> GetRelease(Guid releaseId)
        {
            var release = await dbContext.Releases
                 .To<GetReleaseResourceModel>()
                .SingleOrDefaultAsync(x => x.Id == releaseId);

            release.CoverArt = await releaseFilesService.GetReleaseCoverArt(release.Id);

            return release;

        }

        public async Task<Release> CreateRelease(CreateReleaseInputModel inputModel, Guid formSessionId)
        {

            var release = inputModel.To<Release>();

            var trackedRelease = await this.dbContext.Releases.AddAsync(release);

            await releaseFilesService.AddFilesForRelease(release.Id, formSessionId);

            await this.AddStylesForRelease(release.Id, inputModel.StyleIds);

            await dbContext.SaveChangesAsync();

            return trackedRelease.Entity;
        }

        private async Task AddStylesForRelease(Guid releaseId, ICollection<int> styleIds)
        {
            foreach (var styleId in styleIds)
            {
                var styleRelease = new StyleRelease()
                {
                    ReleaseId = releaseId,
                    StyleId = styleId
                };

                await dbContext.StyleReleases.AddAsync(styleRelease);
            }


        }


    }
}
