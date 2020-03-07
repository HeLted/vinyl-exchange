namespace VinylExchange.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MainServices.Genres;
    using VinylExchange.Web.Models.ResourceModels.Genres;

    public class GenresController : ApiController
    {
        private readonly IGenresService genreService;

        private readonly ILoggerService loggerService;

        public GenresController(IGenresService genreService, ILoggerService loggerService)
        {
            this.genreService = genreService;
            this.loggerService = loggerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            try
            {
                IEnumerable<GetAllGenresResourceModel> genres =
                    await this.genreService.GetAllGenres();

                return this.Ok(genres);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}