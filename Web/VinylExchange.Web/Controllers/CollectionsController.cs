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
                var resourceModel = await collectionsService.AddToCollection<AddToCollectionResourceModel>(
                    inputModel.VinylGrade,
                    inputModel.SleeveGrade,
                    inputModel.Description,
                    inputModel.ReleaseId,
                    GetUserId(User));

                return Created(resourceModel);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("DoesUserCollectionContainRelease")]
        public async Task<ActionResult<bool>> DoesUserCollectionContainRelease(Guid releaseId)
        {
            try
            {
                return await collectionsService.DoesUserCollectionContainRelease(
                    releaseId,
                    GetUserId(User));
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCollectionItemResourceModel>> Get(Guid id)
        {
            try
            {
                var collectionItemModel =
                    await collectionsService.GetCollectionItem<GetCollectionItemResourceModel>(id);

                if (collectionItemModel == null)
                {
                    return NotFound();
                }

                return collectionItemModel;
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserCollection")]
        public async Task<ActionResult<IEnumerable<GetUserCollectionResourceModel>>> GetUserCollection()
        {
            try
            {
                return await collectionsService.GetUserCollection<GetUserCollectionResourceModel>(
                    GetUserId(User));
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                var collectionItemInfoModel =
                    await collectionsService.GetCollectionItem<GetCollectionItemInfoUtilityModel>(id);

                if (collectionItemInfoModel == null)
                {
                    return NotFound();
                }

                if (collectionItemInfoModel.UserId != GetUserId(User))
                {
                    return Unauthorized();
                }

                var collectionItemRemovedModel =
                    await collectionsService.RemoveCollectionItem<RemoveCollectionItemResourceModel>(
                        collectionItemInfoModel.Id);

                return Ok(collectionItemRemovedModel);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }
    }
}