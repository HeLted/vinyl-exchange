
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
using VinylExchange.Models.Utility;
using VinylExchange.Models.ViewModels.Releases;
using VinylExchange.Services.Files;
using VinylExchange.Services.Files.SettingEnums;
using VinylExchange.Services.Mapping;
using VinylExchange.Services.MemoryCache;

namespace VinylExchange.Services
{
    public class ReleasesService : IReleasesService
    {
        private readonly VinylExchangeDbContext dbContext;
        private readonly MemoryCacheManager cacheManager;
        private readonly IFileManager fileManager;

        public ReleasesService(VinylExchangeDbContext dbContext,
            MemoryCacheManager cacheManager,
            IFileManager fileManager)
        {
            this.dbContext = dbContext;
            this.cacheManager = cacheManager;
            this.fileManager = fileManager;
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

            await dbContext.SaveChangesAsync();


            var formSessionStorage = cacheManager.Get<List<UploadFileUtilityModel>>(inputModel.FormSessionId, null);

            var files = formSessionStorage.OrderBy(x => x.DateTime);

            await fileManager.SaveFiles(files, this.GetType().Name.Replace("Service",String.Empty));

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
