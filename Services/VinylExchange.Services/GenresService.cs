using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Models.ViewModels.Genres;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services
{
   public class GenresService : IGenresService
    {
        private readonly VinylExchangeDbContext dbContext;

        public GenresService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<GetAllGenresViewModel>> GetAllGenres()
        {
            return await dbContext.Genres.To<GetAllGenresViewModel>().ToListAsync();

        }
        
    }
}
