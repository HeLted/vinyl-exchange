using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VinylExchange.Common;
using VinylExchange.Models.InputModels.Collections;
using VinylExchange.Services.Logging;
using VinylExchange.Services.MainServices;


namespace VinylExchange.Controllers
{
    public class CollectionsController: ApiController
    {
        private readonly ICollectionsService collectionsService;
        private readonly ILoggerService loggerService;

        public CollectionsController(ICollectionsService collectionsService , ILoggerService loggerService)
        {
            this.collectionsService = collectionsService;
            this.loggerService = loggerService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(AddToCollectionInputModel inputModel,Guid releaseId, string userId)
        {
           

            if (this.User.FindFirst("sub").Value == userId) 
            {
                try
                {
                    var collectionItem = await this.collectionsService.AddToCollection(inputModel, releaseId, userId);

                    return CreatedAtRoute("Default", new { id = collectionItem.Id, message = "Succesfully Added To My Collection" });
                }
                catch (Exception ex)
                {
                    loggerService.LogException(ex);
                    return BadRequest(new { message = UserErrorMessages.ServerErrorMessage });
                }

            }
            else
            {
                return Unauthorized();
            }
                       
        }


    }
}
