namespace VinylExchange.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MainServices.Genres;
    using VinylExchange.Web.Models.InputModels.Genres;
    using VinylExchange.Web.Models.ResourceModels.Genres;
    using static VinylExchange.Common.Constants.RolesConstants;

    public class GenresController : ApiController
    {
        private readonly IGenresService genresService;

        private readonly ILoggerService loggerService;

        public GenresController(IGenresService genresService, ILoggerService loggerService)
        {
            this.genresService = genresService;
            this.loggerService = loggerService;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<GetAllGenresResourceModel>>> GetAllGenres()
        {
            try
            {               
                return await this.genresService.GetAllGenres<GetAllGenresResourceModel>();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = Admin)]
        public async Task<ActionResult<CreateGenreResourceModel>> Create(CreateGenreInputModel inputModel)
        {
            try
            {                 
                return this.Created(await this.genresService.CreateGenre<CreateGenreResourceModel>(inputModel));
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Admin)]
        public async Task<ActionResult<RemoveGenreResourceModel>> Remove(int id)
        {
            try
            {                
                return await this.genresService.RemoveGenre<RemoveGenreResourceModel>(id);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}