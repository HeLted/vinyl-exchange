namespace VinylExchange.Services.MainServices.Styles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Models.ResourceModels.Styles;

    public interface IStylesService
    {
        Task<IEnumerable<GetAllStylesResourceModel>> GetAllStylesForGenre(int genreId);
    }
}