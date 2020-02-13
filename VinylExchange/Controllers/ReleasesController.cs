using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VinylExchange.Common;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Services.Logging;
using VinylExchange.Services.MainServices;

namespace VinylExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleasesController : Controller
    {

        private readonly IReleasesService releasesService;
        private readonly ILoggerService loggerService;

        public ReleasesController(IReleasesService releasesService,ILoggerService loggerService)
        {
            this.releasesService = releasesService;
            this.loggerService = loggerService;          
        }


        [HttpGet]
        [Route("GetReleases")]
        public async Task<IActionResult> GetReleases(string searchTerm, int releasesToSkip)
        {
            
            try
            {
                var releases = await this.releasesService.GetReleases(searchTerm, releasesToSkip);
                return Ok(releases);
            }
            catch(Exception ex)
            {
                loggerService.LogException(ex);
                return NotFound(new { message = UserErrorMessages.ServerErrorMessage });
            }
           
                     
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var release = await this.releasesService.GetRelease(id);

                if (release == null)
                {
                    return NotFound(new { message = "Release Not Found" });
                }

                return Ok(release);
            }
            catch(Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest(new { message = UserErrorMessages.ServerErrorMessage });
            }
            
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(AddReleaseInputModel inputModel)
        {
            
            try
            {
                var release = await releasesService.AddRelease(inputModel);
                                           
                return CreatedAtRoute("Default", new { id = release.Id ,message = "Succesfully Added Release"});
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest(new { message = UserErrorMessages.ServerErrorMessage });
            }

        }

    }
}
