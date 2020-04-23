namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.InputModels.Releases;
    using Models.ResourceModels.Releases;
    using Services.Data.MainServices.Releases.Contracts;
    using Services.Logging;

    public class ReleasesController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IReleasesService releasesService;

        public ReleasesController(IReleasesService releasesService, ILoggerService loggerService)
        {
            this.releasesService = releasesService;
            this.loggerService = loggerService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateReleaseResourceModel>> Create(
            CreateReleaseInputModel inputModel,
            Guid formSessionId)
        {
            try
            {
                var resourceModel = await releasesService.CreateRelease<CreateReleaseResourceModel>(
                    inputModel.Artist,
                    inputModel.Title,
                    inputModel.Format,
                    inputModel.Year,
                    inputModel.Label,
                    inputModel.StyleIds,
                    formSessionId);

                return resourceModel;
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetReleaseResourceModel>> Get(Guid id)
        {
            try
            {
                var release = await releasesService.GetRelease<GetReleaseResourceModel>(id);

                if (release == null)
                {
                    return NotFound();
                }

                return release;
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetReleases")]
        public async Task<ActionResult<IEnumerable<GetReleasesResourceModel>>> GetReleases(
            string searchTerm,
            int? filterGenreId,
            [FromQuery(Name = "styleIds")] List<int> styleIds,
            int releasesToSkip)
        {
            try
            {
                return await releasesService.GetReleases<GetReleasesResourceModel>(
                    searchTerm,
                    filterGenreId,
                    styleIds,
                    releasesToSkip);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }
    }
}