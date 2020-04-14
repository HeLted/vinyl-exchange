using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Common.Enumerations;
using VinylExchange.Data.Models;
using VinylExchange.Data.Seeding.Contracts;

namespace VinylExchange.Data.Seeding
{
    public class ReleaseFilesSeeder : ISeeder
    {
        public const string ImagesPath = @"\Image";
        
        public const string AudioFilesPath = @"\Audio";

        public async Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (!dbContext.ReleaseFiles.Any())
            {
                 //Aphex Twin - Selected Ambient Works 85 - 92
            await SeedReleaseFileAsync(dbContext,Guid.Parse("3cc8ee5c-4dc3-4009-b3fc-d768e6a564f8"),FileType.Image,
                "e10ccdba-7f17-4831-a9e9-335a3f2ec96c@---@ZG93bmxvYWQ=.jpg",true);

              await SeedReleaseFileAsync(dbContext,Guid.Parse("3cc8ee5c-4dc3-4009-b3fc-d768e6a564f8"),FileType.Audio,
                "0d8f30d3-67a7-48b6-a2ad-f69d0afebed8@---@QXBoZXggVHdpbiAtIEFnZWlzcG9saXM=.mp3",false);

            }

        }

        private static async Task SeedReleaseFileAsync(VinylExchangeDbContext dbContext, Guid releaseId,FileType fileType,string fileName,bool isPrewiew)
        {
            var releaseFile = new ReleaseFile
            {
                 ReleaseId = releaseId,
                  Path = @"\Releases\" + fileType.ToString() + "\\",
                  FileName = fileName,
                  IsPreview = isPrewiew,
                  FileType = fileType
            };

            await dbContext.ReleaseFiles.AddAsync(releaseFile);

            await dbContext.SaveChangesAsync();
        }
    }
}
