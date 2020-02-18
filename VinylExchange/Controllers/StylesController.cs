using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinylExchange.Services.MainServices;

namespace VinylExchange.Controllers
{
    public class StylesController : ApiController
    {
        private readonly IStylesService styleService;
        public StylesController(IStylesService styleService)
        {
            this.styleService = styleService;
        }

        [HttpGet]
        [Route("GetAllStylesForGenre")]
        public async Task<IActionResult> GetAllStylesForGenre(int genreId)
        {
            var styles = await this.styleService.GetAllStylesForGenre(genreId);
            return Ok(styles);
        }
    }
}
