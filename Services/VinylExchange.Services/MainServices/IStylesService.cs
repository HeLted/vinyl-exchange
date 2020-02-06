using System.Collections.Generic;
using System.Threading.Tasks;
using VinylExchange.Models.ViewModels.Styles;

namespace VinylExchange.Services.MainServices
{
    public interface IStylesService
    {
        Task<IEnumerable<GetAllStylesViewModel>> GetAllStylesForGenre(int genreId);
    }
}
