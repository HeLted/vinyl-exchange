namespace VinylExchange.Services.MainServices.Genres
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Data;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.Genres;

    public class GenresService : IGenresService
    {
        private readonly VinylExchangeDbContext dbContext;

        public GenresService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GetAllGenresResourceModel>> GetAllGenres()
        {
            return await this.dbContext.Genres.To<GetAllGenresResourceModel>().ToListAsync();
        }
    }
}