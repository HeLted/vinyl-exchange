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
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.MainServices.Releases
{
    public class ReleasesService : IReleasesService
    {
        private const int releasesToTake = 10;

        private readonly VinylExchangeDbContext dbContext;
        private readonly IReleaseFilesService releaseFilesService;

        public ReleasesService(VinylExchangeDbContext dbContext,IReleaseFilesService releaseFilesService)
        {
            this.dbContext = dbContext;
            this.releaseFilesService = releaseFilesService;
        }

        public async Task<IEnumerable<GetAllReleasesResourceModel>> GetReleases(string searchTerm,IEnumerable<int> filterStyleIds, int releasesToSkip)
        {
        
            List<GetAllReleasesResourceModel> releases = null;

            if(searchTerm == null)
            {
                releases  = await dbContext.Releases
                .Where(r => r.Styles.Any(s => filterStyleIds.Contains(s.StyleId)) || filterStyleIds.Count() == 0)
                .Skip(releasesToSkip)
                .Take(releasesToTake)
                .To<GetAllReleasesResourceModel>()
                .ToListAsync();
            }
            else
            {
                releases = await dbContext.Releases
               .Where(r => r.Artist.Contains(searchTerm) || r.Title.Contains(searchTerm))
               .Where(r=> r.Styles.Any(s=> filterStyleIds.Contains(s.StyleId)) || filterStyleIds.Count() == 0)
               .Skip(releasesToSkip)
               .Take(releasesToTake)
                .To<GetAllReleasesResourceModel>()
                .ToListAsync();

            }

            releases.ForEach( r => {
                r.CoverArt = releaseFilesService.GetReleaseCoverArt(r.Id).GetAwaiter().GetResult();               
            });
                        
            return releases;
        }


        public async Task<GetReleaseResourceModel> GetRelease(Guid releaseId)
        {
            var release =  await dbContext.Releases
                 .To<GetReleaseResourceModel>()
                .SingleOrDefaultAsync(x => x.Id == releaseId);

            release.CoverArt = await  releaseFilesService.GetReleaseCoverArt(release.Id);

            return release;
                                 
        }

        public async Task<Release> CreateRelease(CreateReleaseInputModel inputModel,Guid formSessionId)
        {
            
            Release release = new Release()
            {
                Artist = inputModel.Artist,
                Title = inputModel.Title,
                Format = inputModel.Format,
                Label = inputModel.Label,
                Year = inputModel.Year,

            };

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
