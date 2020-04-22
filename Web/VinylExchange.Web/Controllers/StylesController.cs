namespace VinylExchange.Web.Controllers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Common.Constants;
    using VinylExchange.Services.Data.MainServices.Styles;
    using VinylExchange.Services.Logging;
    using VinylExchange.Web.Models.InputModels.Styles;
    using VinylExchange.Web.Models.ResourceModels.Styles;

    #endregion

    public class StylesController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IStylesService stylesService;

        public StylesController(IStylesService stylesService, ILoggerService loggerService)
        {
            this.stylesService = stylesService;
            this.loggerService = loggerService;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<CreateStyleResourceModel>> Create(CreateStyleInputModel inputModel)
        {
            try
            {
                var resourceModel =
                    await this.stylesService.CreateStyle<CreateStyleResourceModel>(inputModel.Name, inputModel.GenreId);

                return this.Created(resourceModel);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAllStylesForGenre")]
        public async Task<ActionResult<IEnumerable<GetAllStylesForGenreResourceModel>>> GetAllStylesForGenre(
            int? genreId)
        {
            try
            {
                return await this.stylesService.GetAllStylesForGenre<GetAllStylesForGenreResourceModel>(genreId);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<RemoveStyleResourceModel>> Remove(int id)
        {
            try
            {
                return await this.stylesService.RemoveStyle<RemoveStyleResourceModel>(id);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }
    }
}