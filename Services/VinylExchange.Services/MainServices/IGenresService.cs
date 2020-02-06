using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Models.ViewModels.Genres;

namespace VinylExchange.Services.MainServices
{
    public interface IGenresService
    {
        Task<IEnumerable<GetAllGenresViewModel>> GetAllGenres();
    }
}
