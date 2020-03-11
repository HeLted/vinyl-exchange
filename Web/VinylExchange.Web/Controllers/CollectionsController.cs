﻿namespace VinylExchange.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MainServices.Collections;
    using VinylExchange.Web.Models.InputModels.Collections;
    using VinylExchange.Web.Models.ResourceModels.Collections;
    using VinylExchange.Web.Models.Utility;

    [Authorize]
    public class CollectionsController : ApiController
    {
        private readonly ICollectionsService collectionsService;

        private readonly ILoggerService loggerService;

        public CollectionsController(ICollectionsService collectionsService, ILoggerService loggerService)
        {
            this.collectionsService = collectionsService;
            this.loggerService = loggerService;
        }

        [HttpPost]
        public async Task<ActionResult<AddToCollectionResourceModel>> Add(AddToCollectionInputModel inputModel, Guid releaseId)
        {
            try
            {
                AddToCollectionResourceModel collectionItemModel = await this.collectionsService.AddToCollection<AddToCollectionResourceModel>(
                                                    inputModel,
                                                    releaseId,
                                                    this.GetUserId(this.User));

                return this.Created(collectionItemModel);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("DoesUserCollectionContainRelease")]
        public async Task<ActionResult<bool>> DoesUserCollectionContainRelease(Guid releaseId)
        {
            try
            {
                bool doesUserCollectionContainRelease =
                    await this.collectionsService.DoesUserCollectionContainRelease(
                        releaseId,
                        this.GetUserId(this.User));

                return doesUserCollectionContainRelease;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCollectionItemResourceModel>> Get(Guid id)
        {
            try
            {
                GetCollectionItemResourceModel collectionItemModel = await this.collectionsService.GetCollectionItem<GetCollectionItemResourceModel>(id);

                if (collectionItemModel == null)
                {
                    return this.NotFound();
                }

                return collectionItemModel;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserCollection")]
        public async Task<ActionResult<IEnumerable<GetUserCollectionResourceModel>>> GetUserCollection()
        {
            try
            {
                List<GetUserCollectionResourceModel> userCollection =
                    await this.collectionsService
                    .GetUserCollection<GetUserCollectionResourceModel>(this.GetUserId(this.User));

                return userCollection;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                GetCollectionItemInfoUtilityModel collectionItemInfoModel =
                    await this.collectionsService.GetCollectionItemInfo<GetCollectionItemInfoUtilityModel>(id);

                if (collectionItemInfoModel == null)
                {
                    return this.NotFound();
                }

                if (collectionItemInfoModel.UserId != this.GetUserId(this.User))
                {
                    return this.Unauthorized();
                }

                RemoveCollectionItemResourceModel collectionItemRemovedModel =
                    await this.collectionsService.RemoveCollectionItem<RemoveCollectionItemResourceModel>(collectionItemInfoModel.Id);

                return this.Ok(collectionItemRemovedModel);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}