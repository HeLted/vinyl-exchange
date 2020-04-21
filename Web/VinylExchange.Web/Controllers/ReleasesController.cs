namespace VinylExchange.Web.Controllers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Services.Data.MainServices.Releases;
    using VinylExchange.Services.Data.MainServices.Releases.Contracts;
    using VinylExchange.Services.Logging;
    using VinylExchange.Web.Models.InputModels.Releases;
    using VinylExchange.Web.Models.ResourceModels.Releases;

    #endregion

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
                var resourceModel = await this.releasesService.CreateRelease<CreateReleaseResourceModel>(
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
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetReleaseResourceModel>> Get(Guid id)
        {
            try
            {
                var release = await this.releasesService.GetRelease<GetReleaseResourceModel>(id);

                if (release == null)
                {
                    return this.NotFound();
                }

                return release;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
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
                return await this.releasesService.GetReleases<GetReleasesResourceModel>(
                           searchTerm,
                           filterGenreId,
                           styleIds,
                           releasesToSkip);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}