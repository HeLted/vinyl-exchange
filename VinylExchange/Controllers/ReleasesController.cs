using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Services;
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
        [Route("GetAllReleases")]
        public async Task<IActionResult> GetAllReleases()
        {

            var releases = await this.releasesService.GetAllReleases();
            return Ok(releases);
        }

        [HttpGet]
        [Route("SearchReleases")]
        public async Task<IActionResult> SearchReleases(string searchTerm,int releasesToSkip)
        {
            var releases = await this.releasesService.SearchReleases(searchTerm, releasesToSkip);
        
            return Ok(releases);
        }

        [HttpPost]
        [Route("AddRelease")]
        public async Task<ActionResult> AddRelease(AddReleaseInputModel inputModel)
        {
            var release = await releasesService.AddRelease(inputModel);

            var returnObject = new
            {
                message = "Sucesfully Added Release",
                id = release.Id

            };

            return Json(returnObject);
        }

        [HttpGet]
        [Route("GetReleasesCount")]
        public async Task<ActionResult> GetReleasesCount()
        {
           return Json(await releasesService.GetAllReleasesCount());

        }




    }
}
