namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MainServices.Genres;
    using VinylExchange.Web.Models.InputModels.Genres;
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
        
        public async Task<ActionResult<IEnumerable<GetAllGenresResourceModel>>> GetAllGenres()
        {
            try
            {
                List<GetAllGenresResourceModel> genres = await this.genreService.GetAllGenres<GetAllGenresResourceModel>();

                return genres;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CreateGenreResourceModel>> Create(CreateGenreInputModel inputModel)
        {
            try
            {
                CreateGenreResourceModel genreModel = await this.genreService.CreateGenre<CreateGenreResourceModel>(inputModel);

                return this.Created(genreModel);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}