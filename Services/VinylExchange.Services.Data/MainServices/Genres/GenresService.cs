namespace VinylExchange.Services.Data.MainServices.Genres
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Constants;
    using Contracts;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;

    public class GenresService : IGenresService, IGenresEntityRetriever
    {
        private readonly VinylExchangeDbContext dbContext;

        public GenresService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TModel> CreateGenre<TModel>(string name)
        {
            var genre = new Genre {Name = name};

            var trackedGenre = await dbContext.Genres.AddAsync(genre);

            await dbContext.SaveChangesAsync();

            return trackedGenre.Entity.To<TModel>();
        }

        public async Task<List<TModel>> GetAllGenres<TModel>()
        {
            return await dbContext.Genres.To<TModel>().ToListAsync();
        }

        public async Task<TModel> RemoveGenre<TModel>(int genreId)
        {
            var genre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId);

            if (genre == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.GenreNotFound);
            }

            var removedGenre = dbContext.Genres.Remove(genre).Entity;
            await dbContext.SaveChangesAsync();

            return removedGenre.To<TModel>();
        }

        public async Task<Genre> GetGenre(int? genreId)
        {
            return await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
        }
    }
}