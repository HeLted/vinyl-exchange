namespace VinylExchange.Services.Data.Tests
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;

    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.HelperServices.Releases;
    using VinylExchange.Services.Data.MainServices.Releases;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Services.Files;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Services.MemoryCache;
    using VinylExchange.Web.Models.InputModels.Releases;
    using VinylExchange.Web.Models.ResourceModels.Releases;

    using Xunit;

    #endregion

    [Collection("AutoMapperSetup")]
    public class ReleasesServiceTests
    {
        public const int releasesToTake = 5;

        private readonly VinylExchangeDbContext dbContext;

        private readonly IReleasesService releasesService;

        private readonly CreateReleaseInputModel testCreateReleaseInputModel = new CreateReleaseInputModel
            {
                Artist = "TestArtist",
                Title = "Test Title",
                Year = "1993",
                Format = "Lp",
                Label = "TestLabel",
                StyleIds = new HashSet<int> { 1, 2, 3 }
            };

        public ReleasesServiceTests()
        {
            this.dbContext = DbFactory.CreateVinylExchangeDbContext();
            this.releasesService = new ReleasesService(
                this.dbContext,
                new ReleaseFilesService(
                    this.dbContext,
                    new FileManager(
                        new MemoryCacheFileService(
                            new MemoryCacheManager(new MemoryCache(new MemoryCacheOptions()))))));

            Task.Run(async () => { await this.AddReleasesTestData(); }).Wait();
        }

        [Fact]
        public async Task CreateReleaseShouldCreateRelease()
        {
            var createdReleaseModel = await this.releasesService.CreateRelease<CreateReleaseResourceModel>(
                                          this.testCreateReleaseInputModel,
                                          Guid.NewGuid());

            await this.dbContext.SaveChangesAsync();

            var createdRelease = await this.dbContext.Releases.FirstOrDefaultAsync(r => r.Id == createdReleaseModel.Id);

            Assert.True(createdRelease != null);
        }

        [Fact]
        public async Task CreateReleaseShouldCreateReleaseWithCorrectData()
        {
            var createdReleaseModel = await this.releasesService.CreateRelease<CreateReleaseResourceModel>(
                                          this.testCreateReleaseInputModel,
                                          Guid.NewGuid());

            await this.dbContext.SaveChangesAsync();

            var createdRelease = await this.dbContext.Releases.FirstOrDefaultAsync(r => r.Id == createdReleaseModel.Id);

            Assert.Equal(this.testCreateReleaseInputModel.Artist, createdRelease.Artist);
            Assert.Equal(this.testCreateReleaseInputModel.Title, createdRelease.Title);
            Assert.Equal(this.testCreateReleaseInputModel.Year, createdRelease.Year);
            Assert.Equal(this.testCreateReleaseInputModel.Format, createdRelease.Format);
            Assert.Equal(this.testCreateReleaseInputModel.Label, createdRelease.Label);
            Assert.Equal(
                string.Join(string.Empty, this.testCreateReleaseInputModel.StyleIds),
                string.Join(string.Empty, createdRelease.Styles.Select(sr => sr.StyleId)));
        }

        [Fact]
        public async Task GetReleaseoShouldReturnNullIfProvidedReleaseIdIsNotExistingInDb()
        {
            var createdRelease = (await this.dbContext.Releases.AddAsync(new Release())).Entity;

            await this.dbContext.SaveChangesAsync();

            var returnedReleaseModel = await this.releasesService.GetRelease<GetReleaseResourceModel>(Guid.NewGuid());

            Assert.Null(returnedReleaseModel);
        }

        [Fact]
        public async Task GetReleaseShouldGetRelease()
        {
            var createdRelease = (await this.dbContext.Releases.AddAsync(new Release())).Entity;

            await this.dbContext.SaveChangesAsync();

            var returnedReleaseModel =
                await this.releasesService.GetRelease<GetReleaseResourceModel>(createdRelease.Id);

            Assert.Equal(createdRelease.Id, returnedReleaseModel.Id);
        }

        [Theory]
        [InlineData("Aphex", 1, new[] { 5 }, 4)]
        [InlineData("Aph", 1, new[] { 5, 6 }, 5)]
        [InlineData("", 1, new[] { 7 }, 2)]
        [InlineData("Tiesto", 1, new[] { 10 }, 0)]
        [InlineData("bt", 1, new[] { 2 }, 1)]
        [InlineData("bt", 1, new[] { 2, 4 }, 2)]
        [InlineData("a", 1, new[] { 4 }, 2)]
        public async Task GetReleasesShouldGetFirstFiveReleasesMatchingSearchTermAndGenreFilterAndStyleFilter(
            string searchTerm,
            int filterGenreId,
            IEnumerable<int> filterStyleIds,
            int expectedMatchingReleasesCount)
        {
            var releasesModelsToCompare = await this.dbContext.Releases
                                              .Where(r => r.Styles.Any(s => s.Style.GenreId == filterGenreId))
                                              .Where(
                                                  r => r.Artist.Contains(
                                                           searchTerm,
                                                           StringComparison.InvariantCultureIgnoreCase)
                                                       || r.Title.Contains(
                                                           searchTerm,
                                                           StringComparison.InvariantCultureIgnoreCase))
                                              .Where(
                                                  r => r.Styles.Any(
                                                      sr => filterStyleIds.Contains(sr.StyleId)
                                                            && r.Styles.All(sr => sr.Style.GenreId == filterGenreId)))
                                              .Take(releasesToTake).To<GetReleaseResourceModel>().ToListAsync();

            var releaseModels = await this.releasesService.GetReleases<GetReleaseResourceModel>(
                                    searchTerm,
                                    filterGenreId,
                                    filterStyleIds,
                                    0);

            Assert.True(releaseModels.Count == expectedMatchingReleasesCount);
            Assert.Equal(
                string.Join(string.Empty, releasesModelsToCompare.Select(r => r.Id.ToString())),
                string.Join(string.Empty, releaseModels.Select(r => r.Id.ToString())));
        }

        [Theory]
        [InlineData("Aphex", 5)]
        [InlineData("tiesto", 3)]
        [InlineData("rwqrrwrqwr", 0)]
        [InlineData("test", 0)]
        [InlineData("Eminem", 2)]
        [InlineData("eminem", 2)]
        [InlineData("8", 1)]
        [InlineData("Armin", 1)]
        [InlineData("Chemical Bro", 2)]
        [InlineData("this binary universe", 1)]
        [InlineData("bt", 2)]
        [InlineData("Fatboy", 1)]
        [InlineData("escm", 1)]
        [InlineData("nothing else", 1)]
        [InlineData("metalica", 1)]
        public async Task GetReleasesShouldGetFirstFiveReleasesMatchingSearchTermWithNoGenreAndStyleFilterProvided(
            string searchTerm,
            int expectedMatchingReleasesCount)
        {
            var releasesModelsToCompare = await this.dbContext.Releases
                                              .Where(
                                                  r => r.Artist.Contains(
                                                           searchTerm,
                                                           StringComparison.InvariantCultureIgnoreCase)
                                                       || r.Title.Contains(
                                                           searchTerm,
                                                           StringComparison.InvariantCultureIgnoreCase))
                                              .Take(releasesToTake).To<GetReleaseResourceModel>().ToListAsync();

            var releaseModels =
                await this.releasesService.GetReleases<GetReleaseResourceModel>(searchTerm, null, new List<int>(), 0);

            Assert.True(releaseModels.Count == expectedMatchingReleasesCount);
            Assert.Equal(
                string.Join(string.Empty, releasesModelsToCompare.Select(r => r.Id.ToString())),
                string.Join(string.Empty, releaseModels.Select(r => r.Id.ToString())));
        }

        [Fact]
        public async Task
            GetReleasesShouldGetFirstFiveReleasesWithNoSearchTermAndNoGenreFilterAndNoStyleFilterProvided()
        {
            var releasesModelsToCompare = await this.dbContext.Releases
                                              .Take(5).To<GetReleaseResourceModel>().ToListAsync();

            var releaseModels =
                await this.releasesService.GetReleases<GetReleaseResourceModel>(null, null, new List<int>(), 0);

            Assert.True(releaseModels.Count == releasesToTake);
            Assert.Equal(
                string.Join(string.Empty, releasesModelsToCompare.Select(r => r.Id.ToString())),
                string.Join(string.Empty, releaseModels.Select(r => r.Id.ToString())));
        }

        [Theory]
        [InlineData(20, 5)]
        [InlineData(25, 1)]
        [InlineData(23, 3)]
        [InlineData(10, 5)]
        [InlineData(5, 5)]
        public async Task GetReleasesShouldSkipGivenNumberOfReleasesAndGetNextFiveReleases(
            int releasesToSkip,
            int expectedMatchingReleasesCount)
        {
            var releasesModelsToCompare = await this.dbContext.Releases.Skip(releasesToSkip).Take(releasesToTake)
                                              .To<GetReleaseResourceModel>().ToListAsync();

            var releaseModels = await this.releasesService.GetReleases<GetReleaseResourceModel>(
                                    null,
                                    null,
                                    new List<int>(),
                                    releasesToSkip);

            Assert.True(releaseModels.Count == expectedMatchingReleasesCount);
            Assert.Equal(
                string.Join(string.Empty, releasesModelsToCompare.Select(r => r.Id.ToString())),
                string.Join(string.Empty, releaseModels.Select(r => r.Id.ToString())));
        }

        [Theory]
        [InlineData(1, new[] { 2, 5 }, 0, 5)]
        [InlineData(1, new[] { 2, 5 }, 5, 5)]
        [InlineData(1, new[] { 2, 5 }, 10, 1)]
        [InlineData(2, new[] { 8, 9, 10 }, 0, 3)]
        [InlineData(2, new[] { 8, 10 }, 0, 2)]
        [InlineData(2, new[] { 8 }, 0, 1)]
        public async Task
            GetReleasesShouldSkipSkipCountAndGetNextFiveReleasesMatchingGenreFilterAndStyleFilterWithNoSearchTerm(
                int filterGenreId,
                IEnumerable<int> filterStyleIds,
                int releasesToSkip,
                int expectedMatchingReleasesCount)
        {
            var releasesModelsToCompare = await this.dbContext.Releases
                                              .Where(r => r.Styles.Any(s => s.Style.GenreId == filterGenreId))
                                              .Where(
                                                  r => r.Styles.Any(
                                                      sr => filterStyleIds.Contains(sr.StyleId)
                                                            && r.Styles.All(sr => sr.Style.GenreId == filterGenreId)))
                                              .Skip(releasesToSkip).Take(releasesToTake).To<GetReleaseResourceModel>()
                                              .ToListAsync();
            var releaseModels = await this.releasesService.GetReleases<GetReleaseResourceModel>(
                                    null,
                                    filterGenreId,
                                    filterStyleIds,
                                    releasesToSkip);

            Assert.True(releaseModels.Count == expectedMatchingReleasesCount);
            Assert.Equal(
                string.Join(string.Empty, releasesModelsToCompare.Select(r => r.Id.ToString())),
                string.Join(string.Empty, releaseModels.Select(r => r.Id.ToString())));
        }

        [Theory]
        [InlineData(1, 0, 5)]
        [InlineData(1, 3, 5)]
        [InlineData(1, 5, 5)]
        [InlineData(1, 19, 2)]
        [InlineData(1, 21, 0)]
        [InlineData(2, 0, 3)]
        [InlineData(2, 1, 2)]
        [InlineData(2, 3, 0)]
        [InlineData(3, 0, 2)]
        [InlineData(3, 2, 0)]
        public async Task
            GetReleasesShouldSkipSkipCountAndGetNextFiveReleasesMatchingGenreFilterWithNoSearchTermAndNoStyleFilterProvided(
                int filterGenreId,
                int releaseToSkip,
                int expectedMatchingReleasesCount)
        {
            var releasesModelsToCompare = await this.dbContext.Releases
                                              .Where(r => r.Styles.Any(s => s.Style.GenreId == filterGenreId))
                                              .Skip(releaseToSkip).Take(releasesToTake).To<GetReleaseResourceModel>()
                                              .ToListAsync();

            var releaseModels = await this.releasesService.GetReleases<GetReleaseResourceModel>(
                                    null,
                                    filterGenreId,
                                    new List<int>(),
                                    releaseToSkip);

            Assert.True(releaseModels.Count == expectedMatchingReleasesCount);
            Assert.Equal(
                string.Join(string.Empty, releasesModelsToCompare.Select(r => r.Id.ToString())),
                string.Join(string.Empty, releaseModels.Select(r => r.Id.ToString())));
        }

        [Theory]
        [InlineData("Aphex", 1, 0, 5)]
        [InlineData("Aphex", 1, 4, 2)]
        [InlineData("Aphex", 1, 5, 1)]
        [InlineData("Aphex", 2, 0, 0)]
        [InlineData("tiesto", 1, 0, 3)]
        [InlineData("Metalica", 2, 0, 1)]
        [InlineData("Metalica", 1, 0, 0)]
        public async Task
            GetReleasesShouldSkipSkipCountAndGetNextFiveReleasesMatchingSearchTermAndGenreFilterWithNoStyleFilterProvided(
                string searchTerm,
                int filterGenreId,
                int releaseToSkip,
                int expectedMatchingReleasesCount)
        {
            var releasesModelsToCompare = await this.dbContext.Releases
                                              .Where(r => r.Styles.Any(s => s.Style.GenreId == filterGenreId))
                                              .Where(
                                                  r => r.Artist.Contains(
                                                           searchTerm,
                                                           StringComparison.InvariantCultureIgnoreCase)
                                                       || r.Title.Contains(
                                                           searchTerm,
                                                           StringComparison.InvariantCultureIgnoreCase))
                                              .Where(r => r.Styles.Any(s => s.Style.GenreId == filterGenreId))
                                              .Skip(releaseToSkip).Take(releasesToTake).To<GetReleaseResourceModel>()
                                              .ToListAsync();

            var releaseModels = await this.releasesService.GetReleases<GetReleaseResourceModel>(
                                    searchTerm,
                                    filterGenreId,
                                    new List<int>(),
                                    releaseToSkip);

            Assert.True(releaseModels.Count == expectedMatchingReleasesCount);
            Assert.Equal(
                string.Join(string.Empty, releasesModelsToCompare.Select(r => r.Id.ToString())),
                string.Join(string.Empty, releaseModels.Select(r => r.Id.ToString())));
        }

        private async Task AddReleasesTestData()
        {
            var releases = new List<Release>
                {
                    new Release { Artist = "Aphex Twin", Title = "Drukqs" }, // 0 Drum And Bass, IDM
                    new Release { Artist = "Tiesto", Title = "Traffic" }, // 1 Trance
                    new Release { Artist = "Aphex Twin", Title = "I Care Because You Do" }, // 2 Downtempo, IDM 
                    new Release { Artist = "Squarepusher", Title = "Feed Me Weird Things" }, // 3 Drum And Bass, IDM
                    new Release { Artist = "Eminem", Title = "Marshal Matters 2" }, // 4 Rap
                    new Release { Artist = "Eminem", Title = "8 Mile" }, // 5 Rap
                    new Release { Artist = "Metalica", Title = "Nothing Else Matters" }, // 6 Metal
                    new Release { Artist = "Aphex Twin", Title = "Selected Ambient Works" }, // 7 Ambient
                    new Release { Artist = "Tiesto", Title = "Just be" }, // 8 Trance
                    new Release { Artist = "Tiesto", Title = "Traffic" }, // 9 Trance
                    new Release { Artist = "Gorillaz", Title = "Demon Days" }, // 10 Alternative Rock
                    new Release { Artist = "Amelie Lens", Title = "Lenske" }, // 11 Techno
                    new Release { Artist = "Dustin Zahn", Title = "Stranger To Stability" }, // 12 Techno
                    new Release { Artist = "Linkin Park", Title = "Meteora" }, // 13 Nu Rock
                    new Release { Artist = "Armin Van Buuren", Title = "76" }, // 14 Trance
                    new Release { Artist = "Aphex Twin", Title = "Come To Daddy" }, // 15 IDM, Drill And Bass
                    new Release { Artist = "The Chemical Brothers", Title = "Push The Button" }, // 16 Big Beat
                    new Release { Artist = "The Chemical Brothers", Title = "Surrender" }, // 17 Big Beat
                    new Release { Artist = "Fatboy Slim", Title = "You've Come a Long Way, Baby" }, // 18 Big Beat
                    new Release { Artist = "BT", Title = "This Binary Universe" }, // 19 Downtempo, Ambient
                    new Release { Artist = "BT", Title = "ESCM" }, // 20 Trance 
                    new Release { Artist = "Paul Van Dyk", Title = "Reflections" }, // 21 Trance
                    new Release
                        {
                            Artist = "Squarepusher", Title = "Feed Me Weird Things"
                        }, // 22 Drum And Bass , Drill And Bass
                    new Release { Artist = "Squarepusher", Title = "Ultravisitor" }, // 23 Drum And Bass
                    new Release { Artist = "Aphex Twin", Title = "Selected Ambient Works ||" }, // 24 Ambient
                    new Release { Artist = "Aphex Twin", Title = "Classics" } // 25 IDM
                };

            var genres = new List<Genre>
                {
                    new Genre { Id = 1, Name = "Electronic" },
                    new Genre { Id = 2, Name = "Rock" },
                    new Genre { Id = 3, Name = "Hip Hop" }
                };

            var styles = new List<Style>
                {
                    new Style { Id = 1, Name = "House", GenreId = 1 },
                    new Style { Id = 2, Name = "Trance", GenreId = 1 },
                    new Style { Id = 3, Name = "Drum And Bass", GenreId = 1 },
                    new Style { Id = 4, Name = "Downtempo", GenreId = 1 },
                    new Style { Id = 5, Name = "IDM", GenreId = 1 },
                    new Style { Id = 6, Name = "Ambient", GenreId = 1 },
                    new Style { Id = 7, Name = "Techno", GenreId = 1 },
                    new Style { Id = 8, Name = "Nu Rock", GenreId = 2 },
                    new Style { Id = 9, Name = "Metal", GenreId = 2 },
                    new Style { Id = 10, Name = "Alternative Rock", GenreId = 2 },
                    new Style { Id = 11, Name = "Rap", GenreId = 3 },
                    new Style { Id = 12, Name = "Big Beat", GenreId = 1 },
                    new Style { Id = 13, Name = "Drill and Bass", GenreId = 1 }
                };

            var styleReleases = new List<StyleRelease>
                {
                    new StyleRelease { ReleaseId = releases[0].Id, StyleId = 3 },
                    new StyleRelease { ReleaseId = releases[0].Id, StyleId = 5 },
                    new StyleRelease { ReleaseId = releases[1].Id, StyleId = 2 },
                    new StyleRelease { ReleaseId = releases[2].Id, StyleId = 4 },
                    new StyleRelease { ReleaseId = releases[2].Id, StyleId = 5 },
                    new StyleRelease { ReleaseId = releases[3].Id, StyleId = 3 },
                    new StyleRelease { ReleaseId = releases[3].Id, StyleId = 5 },
                    new StyleRelease { ReleaseId = releases[4].Id, StyleId = 11 },
                    new StyleRelease { ReleaseId = releases[5].Id, StyleId = 11 },
                    new StyleRelease { ReleaseId = releases[6].Id, StyleId = 9 },
                    new StyleRelease { ReleaseId = releases[7].Id, StyleId = 6 },
                    new StyleRelease { ReleaseId = releases[8].Id, StyleId = 2 },
                    new StyleRelease { ReleaseId = releases[9].Id, StyleId = 2 },
                    new StyleRelease { ReleaseId = releases[10].Id, StyleId = 10 },
                    new StyleRelease { ReleaseId = releases[11].Id, StyleId = 7 },
                    new StyleRelease { ReleaseId = releases[12].Id, StyleId = 7 },
                    new StyleRelease { ReleaseId = releases[13].Id, StyleId = 8 },
                    new StyleRelease { ReleaseId = releases[14].Id, StyleId = 2 },
                    new StyleRelease { ReleaseId = releases[15].Id, StyleId = 5 },
                    new StyleRelease { ReleaseId = releases[15].Id, StyleId = 13 },
                    new StyleRelease { ReleaseId = releases[16].Id, StyleId = 12 },
                    new StyleRelease { ReleaseId = releases[17].Id, StyleId = 12 },
                    new StyleRelease { ReleaseId = releases[18].Id, StyleId = 12 },
                    new StyleRelease { ReleaseId = releases[19].Id, StyleId = 4 },
                    new StyleRelease { ReleaseId = releases[19].Id, StyleId = 6 },
                    new StyleRelease { ReleaseId = releases[20].Id, StyleId = 2 },
                    new StyleRelease { ReleaseId = releases[21].Id, StyleId = 2 },
                    new StyleRelease { ReleaseId = releases[22].Id, StyleId = 3 },
                    new StyleRelease { ReleaseId = releases[22].Id, StyleId = 13 },
                    new StyleRelease { ReleaseId = releases[23].Id, StyleId = 3 },
                    new StyleRelease { ReleaseId = releases[24].Id, StyleId = 6 },
                    new StyleRelease { ReleaseId = releases[25].Id, StyleId = 5 }
                };

            await this.dbContext.Genres.AddRangeAsync(genres);
            await this.dbContext.Styles.AddRangeAsync(styles);
            await this.dbContext.Releases.AddRangeAsync(releases);
            await this.dbContext.StyleReleases.AddRangeAsync(styleReleases);

            await this.dbContext.SaveChangesAsync();
        }
    }
}