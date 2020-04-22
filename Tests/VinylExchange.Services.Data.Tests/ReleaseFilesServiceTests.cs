using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Data.Models;
using VinylExchange.Services.Data.HelperServices.Releases;
using VinylExchange.Services.Data.Tests.TestFactories;
using VinylExchange.Services.Files;
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
                 ReleaseId = release.Id                  
            };

            await this.dbContext.ReleaseFiles.AddAsync(releaseFile);

            await this.dbContext.SaveChangesAsync();


        }
    }
}
