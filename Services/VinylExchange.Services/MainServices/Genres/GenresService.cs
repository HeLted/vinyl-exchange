namespace VinylExchange.Services.MainServices.Genres
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Genres;

    public class GenresService : IGenresService
    {
        private readonly VinylExchangeDbContext dbContext;

        public GenresService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TModel> CreateGenre<TModel>(CreateGenreInputModel inputModel)
        {
            Genre genre = inputModel.To<Genre>();

            EntityEntry<Genre> trackedGenre = await this.dbContext.Genres.AddAsync(genre);

            await this.dbContext.SaveChangesAsync();

            return trackedGenre.Entity.To<TModel>();
        }

        public async Task<List<TModel>> GetAllGenres<TModel>()
        {
            return await this.dbContext.Genres.To<TModel>().ToListAsync();
        }
    }
}