namespace VinylExchange.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Data.Models;
    using VinylExchange.Models.InputModels.Collections;
    using VinylExchange.Models.ResourceModels.Collections;
    using VinylExchange.Models.Utility;
    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MainServices.Collections;

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
        public async Task<IActionResult> Add(AddToCollectionInputModel inputModel, Guid releaseId)
        {
            try
            {
                CollectionItem collectionItem =
                    await this.collectionsService.AddToCollection(inputModel, releaseId, this.GetUserId(this.User));

                return this.StatusCode(HttpStatusCode.Created, collectionItem.Id);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("DoesUserCollectionContainRelease")]
        public async Task<IActionResult> DoesUserCollectionContainRelease(Guid releaseId)
        {
            try
            {
                bool doesUserCollectionContainRelease =
                    await this.collectionsService.DoesUserCollectionContainRelease(
                        releaseId,
                        this.GetUserId(this.User));

                return this.Ok(new { doesUserCollectionContainRelease });
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
                GetCollectionItemResourceModel collectionItem =
                    await this.collectionsService.GetCollectionItem(id);

                if (collectionItem == null)
                {
                    return this.NotFound();
                }

                return this.Ok(collectionItem);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserCollection")]
        public async Task<IActionResult> GetUserCollection()
        {
            try
            {
                IEnumerable<GetUserCollectionResourceModel> userCollection = await this.collectionsService.GetUserCollection(this.GetUserId(this.User));

                return this.Ok(userCollection);
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
                    await this.collectionsService.GetCollectionItemInfo(id);

                if (collectionItemInfoModel == null)
                {
                    return this.NotFound();
                }

                if (collectionItemInfoModel.UserId != this.GetUserId(this.User))
                {
                    return this.Unauthorized();
                }

                RemoveCollectionItemResourceModel collectionItemRemovedModel =
                    await this.collectionsService.RemoveCollectionItem(collectionItemInfoModel.Id);

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