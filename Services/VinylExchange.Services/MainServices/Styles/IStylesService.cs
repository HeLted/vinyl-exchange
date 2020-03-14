namespace VinylExchange.Services.MainServices.Styles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Web.Models.InputModels.Styles;

    public interface IStylesService
    {
        Task<List<TModel>> GetAllStylesForGenre<TModel>(int? genreId);

        Task<TModel> CreateStyle<TModel>(CreateStyleInputModel inputModel);

        Task<TModel> RemoveStyle<TModel>(int styleId);
    }
}