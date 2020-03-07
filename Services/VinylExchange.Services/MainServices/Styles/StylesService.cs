namespace VinylExchange.Services.MainServices.Styles
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Data;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.ResourceModels.Styles;

    public class StylesService : IStylesService
    {
        private readonly VinylExchangeDbContext dbContext;

        public StylesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GetAllStylesResourceModel>> GetAllStylesForGenre(int genreId)
        {
            return await this.dbContext.Styles.Where(x => x.GenreId == genreId).To<GetAllStylesResourceModel>()
                       .ToArrayAsync();
        }
    }
}