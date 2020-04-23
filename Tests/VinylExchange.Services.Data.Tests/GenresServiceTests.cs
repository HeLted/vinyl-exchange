namespace VinylExchange.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;
    using Common.Constants;
    using MainServices.Genres;
    using Microsoft.EntityFrameworkCore;
    using TestFactories;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using Web.Models.ResourceModels.Genres;
    using Xunit;

    public class GenresServiceTests
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly GenresService genresService;

        public GenresServiceTests()
        {
            dbContext = DbFactory.CreateDbContext();

            genresService = new GenresService(dbContext);
        }

        [Fact]
        public async Task CreateGenreShouldCreateGenre()
        {
            var createdGenreModel = await genresService.CreateGenre<CreateGenreResourceModel>("House");

            await dbContext.SaveChangesAsync();

            var createdGenre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == createdGenreModel.Id);

            Assert.NotNull(createdGenre);
        }

        [Fact]
        public async Task CreateGenreShouldCreateGenreWithCorrectData()
        {
            var createdGenreModel = await genresService.CreateGenre<CreateGenreResourceModel>("Trance");

            await dbContext.SaveChangesAsync();

            var createdGenre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == createdGenreModel.Id);

            Assert.Equal("Trance", createdGenre.Name);
        }

        [Fact]
        public async Task GetAllGenresShouldGetAllGenres()
        {
            for (var i = 0; i < 3; i++)
            {
                await dbContext.Genres.AddAsync(new Genre());
            }

            await dbContext.SaveChangesAsync();

            var genreModels = await genresService.GetAllGenres<GetAllGenresResourceModel>();

            Assert.True(genreModels.Count == 3);
        }

        [Fact]
        public async Task GetAllGenresShouldReturnEmptyListIfThereAreNoGenresInDb()
        {
            var genreModels = await genresService.GetAllGenres<GetAllGenresResourceModel>();

            Assert.True(genreModels.Count == 0);
        }

        [Fact]
        public async Task RemoveGenreShouldRemoveGenre()
        {
            var genre = (await dbContext.Genres.AddAsync(new Genre {Id = 1})).Entity;

            await dbContext.SaveChangesAsync();

            await genresService.RemoveGenre<RemoveGenreResourceModel>(genre.Id);

            var removeGenre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genre.Id);

            Assert.Null(removeGenre);
        }

        [Fact]
        public async Task RemoveGenreShouldThrowNullReferenceExceptionIfProvidedGenreIdIsNotInDb()
        {
            var rnd = new Random();

            await dbContext.Genres.AddAsync(new Genre {Id = 1});

            await dbContext.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await genresService.RemoveGenre<RemoveGenreResourceModel>(
                    rnd.Next(2, int.MaxValue)));

            Assert.Equal(NullReferenceExceptionsConstants.GenreNotFound, exception.Message);
        }

        [Fact]
        public async Task GetGenreShouldGetGenre()
        {
            var genre = new Genre();

            await dbContext.Genres.AddAsync(genre);

            await dbContext.SaveChangesAsync();

            var returnedGenre = await genresService.GetGenre(genre.Id);

            Assert.NotNull(returnedGenre);
        }
    }
}