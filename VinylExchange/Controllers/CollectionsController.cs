using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VinylExchange.Common;
using VinylExchange.Data.Models;
using VinylExchange.Models.InputModels.Collections;
using VinylExchange.Services.Logging;
using VinylExchange.Services.MainServices;


namespace VinylExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CollectionsController:Controller
    {
        private readonly ICollectionsService collectionsService;
        private readonly ILoggerService loggerService;
        private readonly UserManager<VinylExchangeUser> userManager;

        public CollectionsController(ICollectionsService collectionsService , ILoggerService loggerService,UserManager<VinylExchangeUser> userManager)
        {
            this.collectionsService = collectionsService;
            this.loggerService = loggerService;
            this.userManager = userManager;
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
