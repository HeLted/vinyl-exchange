using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Models.InputModels.Shops;
using VinylExchange.Services.Data.MainServices.Shops;
using VinylExchange.Services.Logging;

namespace VinylExchange.Controllers
{
    public class ShopsController : ApiController
    {
        private readonly IShopsService shopsService;
        private readonly ILoggerService loggerService;

        public ShopsController(IShopsService shopsService, ILoggerService loggerService)
        {
            this.shopsService = shopsService;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [Route("GetReleases")]
        public async Task<IActionResult> GetReleases(string searchTerm, int releasesToSkip)
        {

            try
            {
                var shops = await this.shopsService.GetShops(searchTerm, shopsToSkip);
                return Ok(shops);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return NotFound();
            }


        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateShopInputModel inputModel, Guid formSessionId)
        {
            try
            {
                var shop = await shopsService.CreateShop(inputModel, formSessionId);

                return CreatedAtRoute("Default", new { id = shop.Id });
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }


        }

    }
}
