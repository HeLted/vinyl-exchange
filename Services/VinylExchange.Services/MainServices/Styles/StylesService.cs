namespace VinylExchange.Services.Data.MainServices.Styles
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Styles;

    #endregion

    public class StylesService : IStylesService
    {
        private readonly VinylExchangeDbContext dbContext;

        public StylesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TModel> CreateStyle<TModel>(CreateStyleInputModel inputModel)
        {
            var style = inputModel.To<Style>();

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
                throw new NullReferenceException("Address with this Id doesn't exist");
            }

            var removedStyle = this.dbContext.Styles.Remove(style).Entity;
            await this.dbContext.SaveChangesAsync();

            return removedStyle.To<TModel>();
        }
    }
}