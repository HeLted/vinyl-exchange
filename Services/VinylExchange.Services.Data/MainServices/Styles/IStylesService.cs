namespace VinylExchange.Services.Data.MainServices.Styles
{
    #region

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Web.Models.InputModels.Styles;

    #endregion

    public interface IStylesService
    {
        Task<TModel> CreateStyle<TModel>(CreateStyleInputModel inputModel);

        Task<List<TModel>> GetAllStylesForGenre<TModel>(int? genreId);

        Task<TModel> RemoveStyle<TModel>(int styleId);
    }
}