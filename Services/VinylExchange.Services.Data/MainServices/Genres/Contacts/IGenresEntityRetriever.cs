namespace VinylExchange.Services.Data.MainServices.Genres.Contracts
{
    using System.Threading.Tasks;
    using VinylExchange.Data.Models;

    public interface IGenresEntityRetriever
    {
        Task<Genre> GetGenre(int? genreId);
    }
}