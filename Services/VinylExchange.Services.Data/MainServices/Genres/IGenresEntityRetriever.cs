using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VinylExchange.Data.Models;

namespace VinylExchange.Services.Data.MainServices.Genres
{
    public interface IGenresEntityRetriever
    {
        Task<Genre> GetGenre(int? genreId);
    }
}
