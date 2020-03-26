namespace VinylExchange.Services.Data.MainServices.Genres
{
    #region

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.InputModels.Genres;

    #endregion

    public interface IGenresService
    {
        Task<TModel> CreateGenre<TModel>(CreateGenreInputModel inputModel);

        Task<List<TModel>> GetAllGenres<TModel>();

        Task<TModel> RemoveGenre<TModel>(int genreId);
    }
}