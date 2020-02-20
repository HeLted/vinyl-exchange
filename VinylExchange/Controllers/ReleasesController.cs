using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VinylExchange.Models.InputModels.Releases;
using VinylExchange.Services.Logging;
using VinylExchange.Services.MainServices.Releases;

namespace VinylExchange.Controllers
{
    public class ReleasesController : ApiController
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
        public async Task<IActionResult> GetReleases(string searchTerm , [FromQuery(Name="styleIds")] List<int> styleIds , int releasesToSkip)
        {
            
            try
            {
                var releases = await this.releasesService.GetReleases(searchTerm, styleIds, releasesToSkip);
                return Ok(releases);
            }
            catch(Exception ex)
            {
                loggerService.LogException(ex);
                return NotFound();
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
                    return NotFound();
                }

                return Ok(release);
            }
            catch(Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
            
        }

        [HttpPost]        
        public async Task<IActionResult> Create(CreateReleaseInputModel inputModel,Guid formSessionId)
        {
            try
            {
                var release = await releasesService.CreateRelease(inputModel, formSessionId);

                return CreatedAtRoute("Default", new { id = release.Id});
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }


        }

    }
}
