using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinylExchange.Services.MainServices;

namespace VinylExchange.Controllers
{
    public class GenresController : ApiController
    {

        private readonly IGenresService genreService;
        public  GenresController(IGenresService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        [Route("GetAllGenres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await this.genreService.GetAllGenres();
            return Ok(genres);
        }
    }
}
