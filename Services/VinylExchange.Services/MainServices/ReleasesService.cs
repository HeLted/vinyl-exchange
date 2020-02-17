
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Models.Utility;
using VinylExchange.Models.ViewModels.ReleaseFiles;
using VinylExchange.Models.ViewModels.Releases;
using VinylExchange.Services.Files;
using VinylExchange.Services.HelperServices;
using VinylExchange.Services.Mapping;
using VinylExchange.Services.MemoryCache;

namespace VinylExchange.Services.MainServices
{
    public class ReleasesService : IReleasesService
    {
        private readonly VinylExchangeDbContext dbContext;
        private readonly IReleaseFilesService releaseFilesService;

        public ReleasesService(VinylExchangeDbContext dbContext,IReleaseFilesService releaseFilesService)
        {
            this.dbContext = dbContext;
            this.releaseFilesService = releaseFilesService;
        }

        public async Task<IEnumerable<GetAllReleasesViewModel>> GetReleases(string searchTerm, int releasesToSkip)
        {
        
            List<GetAllReleasesViewModel> releases = null;

            if(searchTerm == null)
            {
                releases  = await dbContext.Releases               
                .Skip(releasesToSkip)
                .Take(5)
                .To<GetAllReleasesViewModel>()
                .ToListAsync();
            }
            else
            {
                releases = await dbContext.Releases
               .Where(x => x.Artist.Contains(searchTerm) || x.Title.Contains(searchTerm))
               .Skip(releasesToSkip)
               .Take(5)
                .To<GetAllReleasesViewModel>()
                .ToListAsync();

            }

            releases.ForEach( r => {
                r.CoverArt = releaseFilesService.GetReleaseCoverArt(r.Id).GetAwaiter().GetResult();               
            });
                        
            return releases;
        }


        public async Task<GetAllReleasesViewModel> GetRelease(Guid releaseId)
        {
            return await dbContext.Releases
                 .To<GetAllReleasesViewModel>()
                .SingleOrDefaultAsync(x => x.Id == releaseId);
                                 
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
