using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Services.HelperServices;

namespace VinylExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleaseImagesController : Controller
    {
        private readonly IReleaseFilesService releaseFilesService;
        public ReleaseImagesController(IReleaseFilesService releaseFilesService)
        {
            this.releaseFilesService = releaseFilesService;
        }
        [HttpGet]
        [Route("GetAllImagesForRelease")]
        public async Task<IActionResult> GetAllImagesForRelease(Guid releaseId)
        {
            return Ok(await releaseFilesService.GetReleaseImages(releaseId));
        }

    }
}
