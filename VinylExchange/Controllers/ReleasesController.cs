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

        public IActionResult AddRelease()
        {
            return View();
        }


        public IActionResult AddRelease(AddReleaseInputModel inputModel)
        {
            this.releasesService.AddRelease(inputModel);

            return Redirect("/Marketplace");
        }

    }
}
