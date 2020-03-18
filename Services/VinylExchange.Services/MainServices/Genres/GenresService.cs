namespace VinylExchange.Services.Data.MainServices.Genres
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VinylExchange.Common.Constants;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Genres;

    #endregion

    public class GenresService : IGenresService
    {
        private readonly VinylExchangeDbContext dbContext;

        public GenresService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TModel> CreateGenre<TModel>(CreateGenreInputModel inputModel)
        {
            var genre = inputModel.To<Genre>();

            var trackedGenre = await this.dbContext.Genres.AddAsync(genre);

            await this.dbContext.SaveChangesAsync();

            return trackedGenre.Entity.To<TModel>();
        }

        public async Task<List<TModel>> GetAllGenres<TModel>()
        {
            return await this.dbContext.Genres.To<TModel>().ToListAsync();
        }

        public async Task<TModel> RemoveGenre<TModel>(int genreId)
        {
            var genre = await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId);

            if (genre == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.GenreNotFound);
            }

            var removedGenre = this.dbContext.Genres.Remove(genre).Entity;
            await this.dbContext.SaveChangesAsync();

            return removedGenre.To<TModel>();
        }
    }
}