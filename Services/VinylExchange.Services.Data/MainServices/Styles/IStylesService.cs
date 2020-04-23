namespace VinylExchange.Services.Data.MainServices.Styles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStylesService
    {
        Task<TModel> CreateStyle<TModel>(string name, int genreId);

        Task<List<TModel>> GetAllStylesForGenre<TModel>(int? genreId);

        Task<TModel> RemoveStyle<TModel>(int styleId);
    }
}