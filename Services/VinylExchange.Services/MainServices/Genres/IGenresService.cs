namespace VinylExchange.Services.MainServices.Genres
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Web.Models.InputModels.Genres;

    public interface IGenresService
    {
        Task<List<TModel>> GetAllGenres<TModel>();

        Task<TModel> CreateGenre<TModel>(CreateGenreInputModel inputModel);

        Task<TModel> RemoveGenre<TModel>(int genreId);
    }
}