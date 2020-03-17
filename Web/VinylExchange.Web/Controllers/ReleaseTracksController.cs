namespace VinylExchange.Web.Controllers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VinylExchange.Services.Data.HelperServices.Releases;   
    using VinylExchange.Services.Logging;
    using VinylExchange.Web.Models.ResourceModels.ReleaseFiles;

    #endregion

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
                return await this.releaseFilesService.GetReleaseTracks(id);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}