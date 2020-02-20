using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Data;
using VinylExchange.Models.ResourceModels.Styles;
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

        public async Task<IEnumerable<GetAllStylesResourceModel>> GetAllStylesForGenre(int genreId)
        {
            return await dbContext.Styles
                .Where(x => x.GenreId == genreId).To<GetAllStylesResourceModel>()
                .ToArrayAsync();
        }

    }
}
