namespace VinylExchange.Services.Data.Tests
{
    #region

    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Genres.Contracts;
    using VinylExchange.Services.Data.MainServices.Styles;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Web.Models.ResourceModels.Styles;

    using Xunit;

    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    public class StylesServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IStylesService stylesService;

        private readonly Mock<IGenresEntityRetriever> genresEntityRetrieverMock;

        public StylesServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.genresEntityRetrieverMock = new Mock<IGenresEntityRetriever>();

            this.stylesService = new StylesService(this.dbContext, this.genresEntityRetrieverMock.Object);
        }

        [Fact]
        public async Task CreateStyleShouldCreateStyle()
        {
            var name = "Electronic";

            var genre = new Genre();

            this.genresEntityRetrieverMock.Setup(x => x.GetGenre(It.IsAny<int?>())).ReturnsAsync(genre);

            var createdStyleModel = await this.stylesService.CreateStyle<CreateStyleResourceModel>(name, genre.Id);

            await this.dbContext.SaveChangesAsync();

            var createdStyle = await this.dbContext.Styles.FirstOrDefaultAsync(s => s.Id == createdStyleModel.Id);

            Assert.NotNull(createdStyle);
        }

        [Fact]
        public async Task CreateStyleShouldCreateStyleWithCorrectData()
        {
            var name = "Electronic";

            var genre = new Genre();

            this.genresEntityRetrieverMock.Setup(x => x.GetGenre(It.IsAny<int?>())).ReturnsAsync(genre);

            var createdStyleModel = await this.stylesService.CreateStyle<CreateStyleResourceModel>(name, genre.Id);

            await this.dbContext.SaveChangesAsync();

            var createdStyle = await this.dbContext.Styles.FirstOrDefaultAsync(s => s.Id == createdStyleModel.Id);

            Assert.Equal(name, createdStyle.Name);
            Assert.Equal(genre.Id, createdStyle.GenreId);
        }

        [Fact]
        public async Task CreateStyleShouldThrowNullReferenceExceptionIfProvidedGenreIdIsNotInDb()
        {
            this.genresEntityRetrieverMock.Setup(x => x.GetGenre(It.IsAny<int>())).ReturnsAsync((Genre)null);

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.stylesService.CreateStyle<CreateStyleResourceModel>("test", 2));

            Assert.Equal(GenreNotFound, exception.Message);
        }

        [Fact]
        public async Task GetAllStylesForGenreShouldGetAllStylesForProvidedValidGenreId()
        {
            var genreId = 38421;
            var secondGenreId = 23123;

            for (var i = 0; i < 5; i++)
            {
                this.dbContext.Styles.Add(new Style { Name = "Electronic", GenreId = genreId });
            }

            this.dbContext.Styles.Add(new Style { Name = "Metal", GenreId = secondGenreId });

            await this.dbContext.SaveChangesAsync();

            var styleModels = await this.stylesService.GetAllStylesForGenre<GetAllStylesForGenreResourceModel>(genreId);

            Assert.True(styleModels.Count == 5);
        }

        [Fact]
        public async Task GetAllStylesForGenreShouldReturnAllStylesWhenGenreIdIsNotProvided()
        {
            var genreId = 38421;
            var secondGenreId = 23123;

            for (var i = 0; i < 5; i++)
            {
                this.dbContext.Styles.Add(new Style { Name = "Electronic", GenreId = genreId });
            }

            this.dbContext.Styles.Add(new Style { Name = "Metal", GenreId = secondGenreId });

            await this.dbContext.SaveChangesAsync();

            var styleModels = await this.stylesService.GetAllStylesForGenre<GetAllStylesForGenreResourceModel>(null);

            Assert.True(styleModels.Count == 6);
        }

        [Fact]
        public async Task GetAllStylesForGenreShouldReturnEmptyListWhenANonExistingGenreIdIsProvided()
        {
            var genreId = 38421;
            var secondGenreId = 23123;

            for (var i = 0; i < 5; i++)
            {
                this.dbContext.Styles.Add(new Style { Name = "Electronic", GenreId = genreId });
            }

            await this.dbContext.SaveChangesAsync();

            var styleModels =
                await this.stylesService.GetAllStylesForGenre<GetAllStylesForGenreResourceModel>(secondGenreId);

            Assert.True(styleModels.Count == 0);
        }

        [Fact]
        public async Task RemoveStyleShouldRemoveStyle()
        {
            var style = (await this.dbContext.Styles.AddAsync(new Style { Id = 5 })).Entity;

            await this.dbContext.SaveChangesAsync();

            await this.stylesService.RemoveStyle<RemoveStyleResourceModel>(style.Id);

            var removeStyle = await this.dbContext.Genres.FirstOrDefaultAsync(s => s.Id == style.Id);

            Assert.Null(removeStyle);
        }

        [Fact]
        public async Task RemoveStyleShouldThrowNullReferenceExceptionIfProvidedStyleIdIsNotInDb()
        {
            var style = (await this.dbContext.Styles.AddAsync(new Style { Id = 5 })).Entity;

            await this.dbContext.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () => await this.stylesService.RemoveStyle<RemoveStyleResourceModel>(23));

            Assert.Equal(StyleNotFound, exception.Message);
        }
    }
}