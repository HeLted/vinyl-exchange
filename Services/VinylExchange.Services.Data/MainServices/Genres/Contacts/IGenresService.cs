namespace VinylExchange.Services.Data.MainServices.Genres.Contracts
{
    #region

    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    public interface IGenresService
    {
        Task<TModel> CreateGenre<TModel>(string name);

        Task<List<TModel>> GetAllGenres<TModel>();

        Task<TModel> RemoveGenre<TModel>(int genreId);
    }
}