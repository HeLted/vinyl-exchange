namespace VinylExchange.Services.MainServices.Genres
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
  
    using VinylExchange.Web.Models.ResourceModels.Genres;

    public interface IGenresService
    {
        Task<IEnumerable<GetAllGenresResourceModel>> GetAllGenres();
    }
}