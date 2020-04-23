namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.ResourceModels.ReleaseFiles;
    using Services.Data.HelperServices.Releases;
    using Services.Logging;

    public class ReleaseTracksController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IReleaseFilesService releaseFilesService;

        public ReleaseTracksController(IReleaseFilesService releaseFilesService, ILoggerService loggerService)
        {
            this.releaseFilesService = releaseFilesService;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [Route("GetAllTracksForRelease/{id}")]
        public async Task<ActionResult<IEnumerable<ReleaseFileResourceModel>>> GetAllTracksForRelease(Guid id)
        {
            try
            {
                return await releaseFilesService.GetReleaseTracks<ReleaseFileResourceModel>(id);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }
    }
}