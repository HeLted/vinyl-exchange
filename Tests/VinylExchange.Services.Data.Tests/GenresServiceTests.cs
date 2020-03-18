namespace VinylExchange.Services.Data.Tests
{
    using System;
    #region

    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VinylExchange.Common.Constants;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Genres;
    using VinylExchange.Services.Data.Tests.TestFactories;
    using VinylExchange.Web.Models.InputModels.Genres;
    using VinylExchange.Web.Models.ResourceModels.Genres;

    using Xunit;

    #endregion

    [Collection("AutoMapperSetup")]
    public class GenresServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IGenresService genresService;

        private readonly CreateGenreInputModel testCreateGenreInputModel =
            new CreateGenreInputModel { Name = "Test Genre" };

        public GenresServiceTests()
        {
            this.dbContext = DbFactory.CreateVinylExchangeDbContext();
            this.genresService = new GenresService(this.dbContext);
        }

        [Fact]
        public async Task CreateGenreShouldCreateGenre()
        {
            var createdGenreModel =
                await this.genresService.CreateGenre<CreateGenreResourceModel>(this.testCreateGenreInputModel);

            await this.dbContext.SaveChangesAsync();

            var createdGenre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Id == createdGenreModel.Id);

            Assert.True(createdGenre != null);
        }

        [Fact]
        public async Task CreateGenreShouldCreateGenreWithCorrectData()
        {
            var createdGenreModel =
                await this.genresService.CreateGenre<CreateGenreResourceModel>(this.testCreateGenreInputModel);

            await this.dbContext.SaveChangesAsync();

            var createdGenre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Id == createdGenreModel.Id);

            Assert.Equal(this.testCreateGenreInputModel.Name, createdGenre.Name);
        }

        [Fact]
        public async Task GetAllGenresShouldGetAllGenres()
        {
            for (int i = 0; i < 3; i++)
            {
                await this.dbContext.Genres.AddAsync(new Genre { });
            }

            await this.dbContext.SaveChangesAsync();

            var genreModels = await this.genresService.GetAllGenres<GetAllGenresResourceModel>();

            Assert.True(genreModels.Count == 3);
        }

        [Fact]
        public async Task RemoveGenreShouldRemoveGenre()
        {
            var genre = (await this.dbContext.Genres.AddAsync(new Genre() { Id = 1 })).Entity;

            await this.dbContext.SaveChangesAsync();

            await this.genresService.RemoveGenre<RemoveGenreResourceModel>(genre.Id);

            var removeGenre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genre.Id);

            Assert.Null(removeGenre);
        }

        [Fact]
        public async Task RemoveGenreShouldThrowNullReferenceExceptionIfProvidedAddressIdDoesntBelongToAnyAddress()
        {
            Random rnd = new Random();

            await this.dbContext.Genres.AddAsync(new Genre() { Id = 1 });

            await this.dbContext.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                                async () =>
                                    await this.genresService.RemoveGenre<RemoveGenreResourceModel>(
                                        rnd.Next(2, int.MaxValue)));

            Assert.Equal(NullReferenceExceptionsConstants.GenreNotFound, exception.Message);
        }
    }
}