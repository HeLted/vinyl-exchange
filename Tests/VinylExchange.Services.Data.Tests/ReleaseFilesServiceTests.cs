namespace VinylExchange.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;
    using Common.Enumerations;
    using Files;
    using HelperServices.Releases;
    using Moq;
    using TestFactories;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using Web.Models.ResourceModels.ReleaseFiles;
    using Xunit;

    public class ReleaseFilesServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IReleaseFilesService releaseFilesService;

        private readonly Mock<IFileManager> fileManagerMock;

        public ReleaseFilesServiceTests()
        {
            dbContext = DbFactory.CreateDbContext();

            fileManagerMock = new Mock<IFileManager>();

            releaseFilesService = new ReleaseFilesService(dbContext, fileManagerMock.Object);
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

            await dbContext.ReleaseFiles.AddAsync(releaseFile);

            await dbContext.SaveChangesAsync();

            var coverArt = await releaseFilesService.GetReleaseCoverArt<ReleaseFileResourceModel>(release.Id);


            Assert.NotNull(coverArt);
        }

        [Fact]
        public async Task GetReleaseCoverArtShouldReturnNullIfReleaseFileIsNotInDb()
        {
            var coverArt = await releaseFilesService.GetReleaseCoverArt<ReleaseFileResourceModel>(Guid.NewGuid());

            Assert.Null(coverArt);
        }

        [Fact]
        public async Task GetReleaseImagesShouldGetReleaseImages()
        {
            var release = new Release();

            for (var i = 0; i < 10; i++)
            {
                var releaseFile = new ReleaseFile
                {
                    ReleaseId = release.Id,
                    FileType = FileType.Image
                };

                await dbContext.ReleaseFiles.AddAsync(releaseFile);
            }

            await dbContext.SaveChangesAsync();

            var releaseImages = await releaseFilesService.GetReleaseImages<ReleaseFileResourceModel>(release.Id);

            Assert.True(releaseImages.Count == 10);
        }

        [Fact]
        public async Task GetReleaseTracksShouldGetReleaseTracks()
        {
            var release = new Release();

            for (var i = 0; i < 10; i++)
            {
                var releaseFile = new ReleaseFile
                {
                    ReleaseId = release.Id,
                    FileType = FileType.Audio
                };

                await dbContext.ReleaseFiles.AddAsync(releaseFile);
            }

            await dbContext.SaveChangesAsync();

            var releaseTracks = await releaseFilesService.GetReleaseTracks<ReleaseFileResourceModel>(release.Id);

            Assert.True(releaseTracks.Count == 10);
        }
    }
}