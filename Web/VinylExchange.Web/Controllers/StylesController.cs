namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MainServices.Styles;
    using VinylExchange.Web.Models.InputModels.Styles;
    using VinylExchange.Web.Models.ResourceModels.Styles;
    using static VinylExchange.Common.Constants.RolesConstants;

    public class StylesController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IStylesService stylesService;

        public StylesController(IStylesService stylesService, ILoggerService loggerService)
        {
            this.stylesService = stylesService;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [Route("GetAllStylesForGenre")]
        public async Task<ActionResult<IEnumerable<GetAllStylesResourceModel>>> GetAllStylesForGenre(int? genreId)
        {
            try
            {
                return await this.stylesService.GetAllStylesForGenre<GetAllStylesResourceModel>(genreId);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = Admin)]
        public async Task<ActionResult<CreateStyleResourceModel>> Create(CreateStyleInputModel inputModel)
        {
            try
            {
                return this.Created(await this.stylesService.CreateStyle<CreateStyleResourceModel>(inputModel));
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