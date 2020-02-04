
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylEchange.Services.Files;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Models.ViewModels.Releases;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services
{
    public class ReleasesService : IReleasesService
    {
        private readonly VinylExchangeDbContext dbContext;
        private readonly IReleaseImageService imageService;
       

        public ReleasesService(VinylExchangeDbContext dbContext, IReleaseImageService imageService)
        {
            this.dbContext = dbContext;
            this.imageService = imageService;
            
        }

        public async Task<IEnumerable<GetAllReleasesViewModel>> GetAllReleases()
        {
            var releases = await dbContext.Releases.To<GetAllReleasesViewModel>().ToListAsync();

            return releases;
        }

        public async Task<IEnumerable<GetAllReleasesViewModel>> SearchReleases(string searchTerm)
        {
            var releases = await dbContext.Releases
                .Where(x=> x.Artist.Contains(searchTerm) || x.Title.Contains(searchTerm))
                .To<GetAllReleasesViewModel>()
                .ToListAsync();
            
            return releases;
        }

        //public async Task<IEnumerable<QuickSearchReleaseViewModel>> GetReleasesForQuickSearch(string searchValue, int[] genreIds, int[] styleIds)
        //{

        //    if (searchValue == null) {
        //        searchValue = String.Empty;
        //    }

        //    if (styleIds.Length == 0 && genreIds.Length == 0) 
        //    {
        //        styleIds = await dbContext.Styles.Select(x=> x.Id).ToArrayAsync();

        //    } else if (styleIds.Length == 0)
        //    {
        //        styleIds = await dbContext.Genres.Where(g => genreIds.Contains(g.Id)).SelectMany(g => g.Styles.Select(s => s.Id)).ToArrayAsync();
        //    }


        //    var releases = await dbContext.Releases
        //       .Where(x => x.Styles.Any(x => styleIds.Contains(x.StyleId)))
        //       .Where(x => x.Artist.Contains(searchValue) || x.Title.Contains(searchValue))
        //       .To<QuickSearchReleaseViewModel>()
        //       .ToListAsync();



        //    foreach (var release in releases)
        //    {
        //        release.CoverArtPath = this.GetReleaseImageFromServer(release.Id);
        //    }


        //    return releases;

        //}

        public async Task<Release> AddRelease(AddReleaseInputModel inputModel)
        {
            Release release = new Release()
            {
                Artist = inputModel.Artist,
                Title = inputModel.Title,
                Format = inputModel.Format,
                Label = inputModel.Label,
                Year = inputModel.Year,

            };

            await this.dbContext.Releases.AddAsync(release);

            await   dbContext.SaveChangesAsync();

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
    }
}
