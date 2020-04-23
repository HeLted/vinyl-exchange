namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.ResourceModels.ReleaseFiles;
    using Services.Data.HelperServices.Releases;
    using Services.Logging;

    public class ReleaseImagesController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IReleaseFilesService releaseFilesService;

        public ReleaseImagesController(IReleaseFilesService releaseFilesService, ILoggerService loggerService)
        {
            this.releaseFilesService = releaseFilesService;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [Route("GetAllImagesForRelease")]
        public async Task<ActionResult<IEnumerable<ReleaseFileResourceModel>>> GetAllImagesForRelease(Guid releaseId)
        {
            try
            {
                return await this.releaseFilesService.GetReleaseImages<ReleaseFileResourceModel>(releaseId);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetCoverArtForRelease")]
        public async Task<ActionResult<ReleaseFileResourceModel>> GetCoverArtForRelease(Guid releaseId)
        {
            try
            {
                return await this.releaseFilesService.GetReleaseCoverArt<ReleaseFileResourceModel>(releaseId);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }
    }
}