namespace VinylExchange.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Models.ResourceModels.ReleaseFiles;
    using VinylExchange.Services.HelperServices.Releases;
    using VinylExchange.Services.Logging;

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
        public async Task<IActionResult> GetAllImagesForRelease(Guid releaseId)
        {
            try
            {
                IEnumerable<ReleaseFileResourceModel> releaseImages = await this.releaseFilesService.GetReleaseImages(releaseId);

                return this.Ok(releaseImages);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetCoverArtForRelease")]
        public async Task<IActionResult> GetCoverArtForRelease(Guid releaseId)
        {
            try
            {
                ReleaseFileResourceModel releaseCoverArt =
                    await this.releaseFilesService.GetReleaseCoverArt(releaseId);

                return this.Ok(releaseCoverArt);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }
    }
}