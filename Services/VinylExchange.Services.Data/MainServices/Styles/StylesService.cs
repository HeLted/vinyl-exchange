namespace VinylExchange.Services.Data.MainServices.Styles
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Common.Constants;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Data.MainServices.Genres;
    using VinylExchange.Services.Mapping;
    using static VinylExchange.Common.Constants.NullReferenceExceptionsConstants;

    #endregion

    public class StylesService : IStylesService
    {
        private readonly VinylExchangeDbContext dbContext;

        private readonly IGenresEntityRetriever genresEntityRetriever;

        public StylesService(VinylExchangeDbContext dbContext,IGenresEntityRetriever genresEntityRetriever)
        {
            this.dbContext = dbContext;
            this.genresEntityRetriever = genresEntityRetriever;
        }

        public async Task<TModel> CreateStyle<TModel>(string name, int genreId)
        {
            var genre = this.genresEntityRetriever.GetGenre(genreId);

            if(genre == null)
            {
                throw new NullReferenceException(GenreNotFound);
            }

            var style = new Style
            {
                Name = name,
                GenreId = genreId
            };

            var trackedStyle = await this.dbContext.Styles.AddAsync(style);

            await this.dbContext.SaveChangesAsync();

            return trackedStyle.Entity.To<TModel>();
        }

        public async Task<List<TModel>> GetAllStylesForGenre<TModel>(int? genreId)
        {
            var stylesQueriable = this.dbContext.Styles.AsQueryable();

            if (genreId != null)
            {
                stylesQueriable = stylesQueriable.Where(x => x.GenreId == genreId);
            }

            return await stylesQueriable.To<TModel>().ToListAsync();
        }

        public async Task<TModel> RemoveStyle<TModel>(int styleId)
        {
            var style = await this.dbContext.Styles.FirstOrDefaultAsync(s => s.Id == styleId);

            if (style == null)
            {
                throw new NullReferenceException(NullReferenceExceptionsConstants.StyleNotFound);
            }

            var removedStyle = this.dbContext.Styles.Remove(style).Entity;

            await this.dbContext.SaveChangesAsync();

            return removedStyle.To<TModel>();
        }
    }
}