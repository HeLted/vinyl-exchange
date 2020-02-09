using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Services.MainServices;

namespace VinylExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleasesController : Controller
    {

        private readonly IReleasesService releasesService;

        public ReleasesController(IReleasesService releasesService)
        {
            this.releasesService = releasesService;
        }


        [HttpGet]
        [Route("GetReleases")]
        public async Task<IActionResult> GetReleases(string searchTerm,int releasesToSkip)
        {
            var releases = await this.releasesService.GetReleases(searchTerm, releasesToSkip);

            if (releases == null)
            {
                return NotFound();
            }

            return Ok(releases);
        }

        [HttpPost]
        [Route("AddRelease")]
        public async Task<IActionResult> AddRelease(AddReleaseInputModel inputModel)
        {
            var release = await releasesService.AddRelease(inputModel);
            
            if(release == null)
            {
                return BadRequest();
            }
                       
            return CreatedAtRoute($"{release.Id}",release);
        }

    }
}
