using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Services;

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
        [Route("GetAllReleases")]
        public async Task<IActionResult> GetAllReleases()
        {

            var releases = await this.releasesService.GetAllReleases();
            return Ok(releases);
        }

        [HttpGet]
        [Route("SearchReleases")]
        public async Task<IActionResult> SearchReleases(string searchTerm)
        {

            var releases = await this.releasesService.SearchReleases(searchTerm);
            return Ok(releases);
        }

        [HttpPost]
        [Route("addrelease")]
        public IActionResult  AddRelease(AddReleaseInputModel inputModel)
        {
            return Json(inputModel);
        }


      

    }
}
