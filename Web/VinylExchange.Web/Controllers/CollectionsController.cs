namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.InputModels.Collections;
    using Models.ResourceModels.Collections;
    using Models.Utility.Collections;
    using Services.Data.MainServices.Collections;
    using Services.Logging;

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
        public async Task<ActionResult<AddToCollectionResourceModel>> Add(AddToCollectionInputModel inputModel)
        {
            try
            {
                var resourceModel = await this.collectionsService.AddToCollection<AddToCollectionResourceModel>(
                    inputModel.VinylGrade,
                    inputModel.SleeveGrade,
                    inputModel.Description,
                    inputModel.ReleaseId, this.GetUserId(this.User));

                return this.Created(resourceModel);
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
                return await this.collectionsService.DoesUserCollectionContainRelease(
                    releaseId, this.GetUserId(this.User));
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
                var collectionItemModel =
                    await this.collectionsService.GetCollectionItem<GetCollectionItemResourceModel>(id);

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
                return await this.collectionsService.GetUserCollection<GetUserCollectionResourceModel>(
                    this.GetUserId(this.User));
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
                var collectionItemInfoModel =
                    await this.collectionsService.GetCollectionItem<GetCollectionItemInfoUtilityModel>(id);

                if (collectionItemInfoModel == null)
                {
                    return this.NotFound();
                }

                if (collectionItemInfoModel.UserId != this.GetUserId(this.User))
                {
                    return this.Unauthorized();
                }

                var collectionItemRemovedModel =
                    await this.collectionsService.RemoveCollectionItem<RemoveCollectionItemResourceModel>(
                        collectionItemInfoModel.Id);

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