namespace VinylExchange.Services.MainServices.Styles
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VinylExchange.Data;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Models.InputModels.Styles;

    public class StylesService : IStylesService
    {
        private readonly VinylExchangeDbContext dbContext;

        public StylesService(VinylExchangeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<TModel>> GetAllStylesForGenre<TModel>(int genreId)
        {
            return await this.dbContext.Styles.Where(x => x.GenreId == genreId).To<TModel>()
                       .ToListAsync();
        }

        public async Task<TModel> CreateStyle<TModel>(CreateStyleInputModel inputModel)
        {
            Style style = inputModel.To<Style>();

            EntityEntry<Style> trackedStyle = await this.dbContext.Styles.AddAsync(style);

            await this.dbContext.SaveChangesAsync();

            return trackedStyle.Entity.To<TModel>();
        }

        public async Task<TModel> RemoveStyle<TModel>(int styleId)
        {
            Style style = await this.dbContext.Styles.FirstOrDefaultAsync(s => s.Id == styleId);

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