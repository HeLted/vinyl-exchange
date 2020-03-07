namespace VinylExchange.Services.MainServices.Genres
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using VinylExchange.Models.ResourceModels.Genres;

    public interface IGenresService
    {
        Task<IEnumerable<GetAllGenresResourceModel>> GetAllGenres();
    }
}