using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinylExchange.Services.HelperServices;
using VinylExchange.Services.HelperServices.Releases;

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
        [Route("GetAllTracksForRelease/{id}")]
        public async Task<IActionResult> GetAllTracksForRelease(Guid id)
        {
            return Ok(await releaseFilesService.GetReleaseTracks(id));
        }        
        
    }
}
