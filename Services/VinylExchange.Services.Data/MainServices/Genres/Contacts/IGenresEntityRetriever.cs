namespace VinylExchange.Services.Data.MainServices.Genres.Contracts
{
    #region

    using System.Threading.Tasks;

    using VinylExchange.Data.Models;

    #endregion

    public interface IGenresEntityRetriever
    {
        Task<Genre> GetGenre(int? genreId);
    }
}