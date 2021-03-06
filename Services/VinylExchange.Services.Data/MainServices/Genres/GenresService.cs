﻿namespace VinylExchange.Services.Data.MainServices.Genres
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

        public async Task<Genre> GetGenre(int? genreId)
        {
            return await this.dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
        }

        public async Task<TModel> CreateGenre<TModel>(string name)
        {
            var genre = new Genre {Name = name};

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