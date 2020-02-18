using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinylExchange.Services.HelperServices;

namespace VinylExchange.Controllers
{
    public class ReleaseImagesController : ApiController
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

        [HttpGet]
        [Route("GetCoverArtForRelease")]
        public async Task<IActionResult> GetCoverArtForRelease(Guid releaseId)
        {
            return Ok(await releaseFilesService.GetReleaseCoverArt(releaseId));
        }

    }
}
