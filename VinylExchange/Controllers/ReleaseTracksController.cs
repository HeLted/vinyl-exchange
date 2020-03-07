namespace VinylExchange.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Models.ResourceModels.ReleaseFiles;
    using VinylExchange.Services.HelperServices.Releases;
    using VinylExchange.Services.Logging;

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
        public async Task<IActionResult> GetAllTracksForRelease(Guid id)
        {
            try
            {
                IEnumerable<ReleaseFileResourceModel> tracks = await this.releaseFilesService.GetReleaseTracks(id);

                return this.Ok(tracks);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}