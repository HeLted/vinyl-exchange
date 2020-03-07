namespace VinylExchange.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MainServices.Releases;
    using VinylExchange.Web.Models.InputModels.Releases;
    using VinylExchange.Web.Models.ResourceModels.Releases;

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
        [Authorize]
        public async Task<IActionResult> Create(CreateReleaseInputModel inputModel, Guid formSessionId)
        {
            try
            {
                Release release = await this.releasesService.CreateRelease(inputModel, formSessionId);

                return this.StatusCode(HttpStatusCode.Created, release.Id);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                GetReleaseResourceModel release =
                    await this.releasesService.GetRelease(id);

                if (release == null)
                {
                    return this.NotFound();
                }

                return this.Ok(release);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetReleases")]
        public async Task<IActionResult> GetReleases(
            string searchTerm,
            [FromQuery(Name = "styleIds")] List<int> styleIds,
            int releasesToSkip)
        {
            try
            {
                IEnumerable<GetReleasesResourceModel> releases =
                    await this.releasesService.GetReleases(searchTerm, styleIds, releasesToSkip);
                return this.Ok(releases);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.NotFound();
            }
        }
    }
}