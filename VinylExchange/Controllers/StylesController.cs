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
    public class StylesController : Controller
    {
        private readonly IStylesService styleService;
        public StylesController(IStylesService styleService)
        {
            this.styleService = styleService;
        }

        [HttpGet]
        [Route("GetAllStylesForGenre")]
        public async Task<IActionResult> GetAllGenres(int genreId)
        {
            var styles = await this.styleService.GetAllStylesForGenre(genreId);
            return Ok(styles);
        }
    }
}
