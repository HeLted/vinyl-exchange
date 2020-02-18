using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinylExchange.Services.HelperServices;

namespace VinylExchange.Controllers
{
    public class ReleaseTracksController : ApiController
    {
        private readonly IReleaseFilesService releaseFilesService;
        public ReleaseTracksController(IReleaseFilesService releaseFilesService)
        {
            this.releaseFilesService = releaseFilesService;
        }
        [HttpGet]
        [Route("GetAllTracksForRelease")]
        public async Task<IActionResult> GetAllTracksForRelease(Guid releaseId)
        {
            return Ok(await releaseFilesService.GetReleaseTracks(releaseId));
        }        
    }
}
