namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.InputModels.Genres;
    using Models.ResourceModels.Genres;
    using Services.Data.MainServices.Genres.Contracts;
    using Services.Logging;

    public class GenresController : ApiController
    {
        private readonly IGenresService genresService;

        private readonly ILoggerService loggerService;

        public GenresController(IGenresService genresService, ILoggerService loggerService)
        {
            this.genresService = genresService;
            this.loggerService = loggerService;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<CreateGenreResourceModel>> Create(CreateGenreInputModel inputModel)
        {
            try
            {
                return Created(await genresService.CreateGenre<CreateGenreResourceModel>(inputModel.Name));
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllGenresResourceModel>>> GetAllGenres()
        {
            try
            {
                return await genresService.GetAllGenres<GetAllGenresResourceModel>();
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<RemoveGenreResourceModel>> Remove(int id)
        {
            try
            {
                return await genresService.RemoveGenre<RemoveGenreResourceModel>(id);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);
                return BadRequest();
            }
        }
    }
}