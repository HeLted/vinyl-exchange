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
                return await this.releaseFilesService.GetReleaseImages(releaseId);
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
                return await this.releaseFilesService.GetReleaseCoverArt(releaseId);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }
    }
}