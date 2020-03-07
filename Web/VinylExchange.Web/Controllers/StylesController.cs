namespace VinylExchange.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MainServices.Styles;
    using VinylExchange.Web.Models.ResourceModels.Styles;

    public class StylesController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IStylesService styleService;

        public StylesController(IStylesService styleService, ILoggerService loggerService)
        {
            this.styleService = styleService;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [Route("GetAllStylesForGenre")]
        public async Task<IActionResult> GetAllStylesForGenre(int genreId)
        {
            try
            {
                IEnumerable<GetAllStylesResourceModel> styles =
                    await this.styleService.GetAllStylesForGenre(genreId);

                return this.Ok(styles);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}