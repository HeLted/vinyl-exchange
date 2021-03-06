﻿namespace VinylExchange.Services.Data.Tests
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
        public GenresServiceTests()
        {
            this.dbContext = DbFactory.CreateDbContext();

            this.genresService = new GenresService(this.dbContext);
        }

        private readonly VinylExchangeDbContext dbContext;

        private readonly GenresService genresService;

        [Fact]
        public async Task CreateGenreShouldCreateGenre()
        {
            var createdGenreModel = await this.genresService.CreateGenre<CreateGenreResourceModel>("House");

            await this.dbContext.SaveChangesAsync();

            var createdGenre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Id == createdGenreModel.Id);

            Assert.NotNull(createdGenre);
        }

        [Fact]
        public async Task CreateGenreShouldCreateGenreWithCorrectData()
        {
            var createdGenreModel = await this.genresService.CreateGenre<CreateGenreResourceModel>("Trance");

            await this.dbContext.SaveChangesAsync();

            var createdGenre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Id == createdGenreModel.Id);

            Assert.Equal("Trance", createdGenre.Name);
        }

        [Fact]
        public async Task GetAllGenresShouldGetAllGenres()
        {
            for (var i = 0; i < 3; i++)
            {
                await this.dbContext.Genres.AddAsync(new Genre());
            }

            await this.dbContext.SaveChangesAsync();

            var genreModels = await this.genresService.GetAllGenres<GetAllGenresResourceModel>();

            Assert.True(genreModels.Count == 3);
        }

        [Fact]
        public async Task GetAllGenresShouldReturnEmptyListIfThereAreNoGenresInDb()
        {
            var genreModels = await this.genresService.GetAllGenres<GetAllGenresResourceModel>();

            Assert.True(genreModels.Count == 0);
        }

        [Fact]
        public async Task GetGenreShouldGetGenre()
        {
            var genre = new Genre();

            await this.dbContext.Genres.AddAsync(genre);

            await this.dbContext.SaveChangesAsync();

            var returnedGenre = await this.genresService.GetGenre(genre.Id);

            Assert.NotNull(returnedGenre);
        }

        [Fact]
        public async Task RemoveGenreShouldRemoveGenre()
        {
            var genre = (await this.dbContext.Genres.AddAsync(new Genre {Id = 1})).Entity;

            await this.dbContext.SaveChangesAsync();

            await this.genresService.RemoveGenre<RemoveGenreResourceModel>(genre.Id);

            var removeGenre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genre.Id);

            Assert.Null(removeGenre);
        }

        [Fact]
        public async Task RemoveGenreShouldThrowNullReferenceExceptionIfProvidedGenreIdIsNotInDb()
        {
            var rnd = new Random();

            await this.dbContext.Genres.AddAsync(new Genre {Id = 1});

            await this.dbContext.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.genresService.RemoveGenre<RemoveGenreResourceModel>(
                    rnd.Next(2, int.MaxValue)));

            Assert.Equal(NullReferenceExceptionsConstants.GenreNotFound, exception.Message);
        }
    }
}