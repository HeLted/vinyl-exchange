﻿namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<CreateReleaseResourceModel>> Create(CreateReleaseInputModel inputModel, Guid formSessionId)
        {
            try
            {
                CreateReleaseResourceModel releaseModel = 
                    await this.releasesService.CreateRelease<CreateReleaseResourceModel>(inputModel, formSessionId);

                return releaseModel;
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
                GetReleaseResourceModel release = await this.releasesService.GetRelease<GetReleaseResourceModel>(id);

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
            [FromQuery(Name = "styleIds")] List<int> styleIds,
            int releasesToSkip)
        {
            try
            {                              
               
                return await this.releasesService
                    .GetReleases<GetReleasesResourceModel>(searchTerm, styleIds, releasesToSkip); 
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.NotFound();
            }
        }
    }
}