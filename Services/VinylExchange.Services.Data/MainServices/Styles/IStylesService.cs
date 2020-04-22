namespace VinylExchange.Services.Data.MainServices.Styles
{
    #region

    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    public interface IStylesService
    {
        Task<TModel> CreateStyle<TModel>(string name, int genreId);

        Task<List<TModel>> GetAllStylesForGenre<TModel>(int? genreId);

        Task<TModel> RemoveStyle<TModel>(int styleId);
    }
}