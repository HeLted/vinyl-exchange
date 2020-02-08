
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

        public async Task<IEnumerable<GetAllReleasesViewModel>> GetAllReleases()
        {
            Thread.Sleep(5000);

            var releases = await dbContext.Releases.Take(5).To<GetAllReleasesViewModel>().ToListAsync();

            releases.ForEach( r => {
                r.CoverArt =  releaseFilesService.GetReleaseCoverArt(r.Id).GetAwaiter().GetResult();            
            });

            return releases;
        }

        public async Task<IEnumerable<GetAllReleasesViewModel>> SearchReleases(string searchTerm, int releasesToSkip)
        {
            Thread.Sleep(5000);
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


        public async Task<Release> AddRelease(AddReleaseInputModel inputModel)
        {
            var formSessionId = inputModel.FormSessionId;

            Release release = new Release()
            {
                Artist = inputModel.Artist,
                Title = inputModel.Title,
                Format = inputModel.Format,
                Label = inputModel.Label,
                Year = inputModel.Year,

            };

            await this.dbContext.Releases.AddAsync(release);

            await dbContext.SaveChangesAsync();

            await releaseFilesService.AddFilesForRelease(release.Id, formSessionId);

            this.AddStylesForRelease(release.Id, inputModel.StyleIds);

            return release;
        }

        private void AddStylesForRelease(Guid releaseId, ICollection<int> styleIds)
        {
            foreach (var styleId in styleIds)
            {
                var styleRelease = new StyleRelease()
                {
                    ReleaseId = releaseId,
                    StyleId = styleId
                };

                dbContext.StyleReleases.Add(styleRelease);
            }

            dbContext.SaveChangesAsync();
        }

        public async Task<int> GetAllReleasesCount() { 
            return await dbContext.Releases.CountAsync();
        }

    }
}
