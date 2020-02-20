using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Models.ResourceModels.Genres;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.MainServices.Genres
{
    public class GenresService : IGenresService
    {
        private readonly VinylExchangeDbContext dbContext;

        public GenresService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<GetAllGenresResourceModel>> GetAllGenres()
        {
            return await dbContext.Genres.To<GetAllGenresResourceModel>().ToListAsync();

        }
        
    }
}
