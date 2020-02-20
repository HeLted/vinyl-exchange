using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using VinylExchange.Common;
using VinylExchange.Models.InputModels.Collections;
using VinylExchange.Services.Logging;
using VinylExchange.Services.MainServices;


namespace VinylExchange.Controllers
{
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
                var collectionItem = await this.collectionsService.AddToCollection(inputModel, releaseId, this.GetUserId(this.User));

                return CreatedAtRoute("Default", new { id = collectionItem.Id });
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
                var collectionItemInfoModel = await this.collectionsService.GetCollectionItemInfo(id);

                if (collectionItemInfoModel == null)
                {
                    return NotFound();
                }

                if (collectionItemInfoModel.UserId != this.GetUserId(this.User))
                {
                    return Unauthorized();
                }

                var collectionItemRemovedModel = await this.collectionsService.RemoveCollectionItem(collectionItemInfoModel.Id);

                return Ok(collectionItemRemovedModel);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }


        }

        [HttpGet]
        [Route("GetUserCollection")]
        public async Task<IActionResult> GetUserCollection()
        {
            try
            {
                var userCollection = await this.collectionsService.GetUserCollection(this.GetUserId(this.User));

                if (userCollection == null)
                {
                    return NotFound();
                }

                return Ok(userCollection);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }


        }

        [HttpGet]
        [Route("DoesUserCollectionContainRelease")]
        public async Task<IActionResult> DoesUserCollectionContainRelease(Guid releaseId)
        {
            try
            {
                bool doesUserCollectionContainRelease  = await this.collectionsService
                    .DoesUserCollectionContainReleas(releaseId,this.GetUserId(this.User));
                
                return Ok(new { doesUserCollectionContainRelease });
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }


        }


    }
}
