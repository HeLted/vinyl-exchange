namespace VinylExchange.Services.Data.Tests
{
    #region

    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Common.Constants;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Styles;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Web.Models.InputModels.Styles;
    using VinylExchange.Web.Models.ResourceModels.Styles;

    using Xunit;

    #endregion

    [Collection("AutoMapperSetup")]
    public class StylesServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly Random randomGenerator;

        private readonly IStylesService stylesService;

        private readonly CreateStyleInputModel testCreateStyleInputModel =
            new CreateStyleInputModel { Name = "Electronic", GenreId = new Random().Next() };

        public StylesServiceTests()
        {
            this.dbContext = DbFactory.CreateVinylExchangeDbContext();
            this.stylesService = new StylesService(this.dbContext);
            this.randomGenerator = new Random();
        }

        [Fact]
        public async Task CreateGenreShouldCreateGenreWithCorrectData()
        {
            var createdStyleModel =
                await this.stylesService.CreateStyle<CreateStyleResourceModel>(this.testCreateStyleInputModel);

            await this.dbContext.SaveChangesAsync();

            var createdStyle = await this.dbContext.Styles.FirstOrDefaultAsync(s => s.Id == createdStyleModel.Id);

            Assert.Equal(this.testCreateStyleInputModel.Name, createdStyle.Name);
            Assert.Equal(this.testCreateStyleInputModel.GenreId, createdStyle.GenreId);
        }

        [Fact]
        public async Task CreateStyleShouldCreateGenre()
        {
            var createdStyleModel =
                await this.stylesService.CreateStyle<CreateStyleResourceModel>(this.testCreateStyleInputModel);

            await this.dbContext.SaveChangesAsync();

            var createdStyle = await this.dbContext.Styles.FirstOrDefaultAsync(s => s.Id == createdStyleModel.Id);

            Assert.NotNull(createdStyle);
        }

        [Fact]
        public async Task GetAllStylesForGenreShouldGetAllStylesForProvidedValidGenreId()
        {
            var genreId = 38421;
            var secondGenreId = 23123;

            for (var i = 0; i < 5; i++) this.dbContext.Styles.Add(new Style { Name = "Electronic", GenreId = genreId });

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

            for (var i = 0; i < 5; i++) this.dbContext.Styles.Add(new Style { Name = "Electronic", GenreId = genreId });

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

            for (var i = 0; i < 5; i++) this.dbContext.Styles.Add(new Style { Name = "Electronic", GenreId = genreId });

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

            Assert.Equal(NullReferenceExceptionsConstants.StyleNotFound, exception.Message);
        }
    }
}