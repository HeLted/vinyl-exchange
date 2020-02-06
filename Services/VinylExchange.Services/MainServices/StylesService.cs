using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Models.ViewModels.Styles;
using VinylExchange.Services.Mapping;

namespace VinylExchange.Services.MainServices
{
   public class StylesService : IStylesService
    {
        private readonly VinylExchangeDbContext dbContext;
        public StylesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GetAllStylesViewModel>> GetAllStylesForGenre(int genreId)
        {
            return await dbContext.Styles
                .Where(x => x.GenreId == genreId).To<GetAllStylesViewModel>()
                .ToArrayAsync();
        }

    }
}
