using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Services;

namespace VinylExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : Controller
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
