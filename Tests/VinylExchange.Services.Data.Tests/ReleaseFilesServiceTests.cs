using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Common.Enumerations;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Services.Data.HelperServices.Releases;
using VinylExchange.Services.Data.Tests.TestFactories;
using VinylExchange.Services.Files;
using VinylExchange.Web.Models.ResourceModels.ReleaseFiles;
using Xunit;

namespace VinylExchange.Services.Data.Tests
{
    public class ReleaseFilesServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IReleaseFilesService releaseFilesService;

        private readonly Mock<IFileManager> fileManagerMock;

        public ReleaseFilesServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.fileManagerMock = new Mock<IFileManager>();

            this.releaseFilesService = new ReleaseFilesService(this.dbContext, this.fileManagerMock.Object);
        }

        [Fact]
        public async Task GetReleaseCoverArtShouldGetReleaseCoverArt()
        {
            var release = new Release();

            var releaseFile = new ReleaseFile
            {
                ReleaseId = release.Id,
                FileType = FileType.Image,
                IsPreview = true
            };

            await this.dbContext.ReleaseFiles.AddAsync(releaseFile);

            await this.dbContext.SaveChangesAsync();

            var coverArt = await this.releaseFilesService.GetReleaseCoverArt<ReleaseFileResourceModel>(release.Id);


            Assert.NotNull(coverArt);

        }

        [Fact]
        public async Task GetReleaseCoverArtShouldReturnNullIfReleaseFileIsNotInDb()
        {
            var coverArt = await this.releaseFilesService.GetReleaseCoverArt<ReleaseFileResourceModel>(Guid.NewGuid());

            Assert.Null(coverArt);
        }
      
        [Fact]
        public async Task GetReleaseImagesShouldGetReleaseImages()
        {
            var release = new Release();

            for (int i = 0; i < 10; i++)
            {
                var releaseFile = new ReleaseFile
                {
                    ReleaseId = release.Id,          
                    FileType = FileType.Image
                };

                await this.dbContext.ReleaseFiles.AddAsync(releaseFile);
            }

            await this.dbContext.SaveChangesAsync();

            var releaseImages =  await this.releaseFilesService.GetReleaseImages<ReleaseFileResourceModel>(release.Id);

            Assert.True(releaseImages.Count == 10);
        }

        [Fact]
        public async Task GetReleaseTracksShouldGetReleaseTracks()
        {
            var release = new Release();

            for (int i = 0; i < 10; i++)
            {
                var releaseFile = new ReleaseFile
                {
                    ReleaseId = release.Id,          
                    FileType = FileType.Audio
                };

                await this.dbContext.ReleaseFiles.AddAsync(releaseFile);
            }

            await this.dbContext.SaveChangesAsync();

            var releaseTracks =  await this.releaseFilesService.GetReleaseTracks<ReleaseFileResourceModel>(release.Id);

            Assert.True(releaseTracks.Count == 10);
        }
    }
}
