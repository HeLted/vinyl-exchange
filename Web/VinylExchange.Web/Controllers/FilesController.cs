namespace VinylExchange.Web.Controllers
{
    #region

    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MemoryCache;
    using VinylExchange.Web.Models.InputModels.Files;
    using VinylExchange.Web.Models.ResourceModels.File;
    using VinylExchange.Web.Models.ResourceModels.Files;

    #endregion

    public class FilesController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IMemoryCacheFileSevice memoryCacheFileSevice;

        public FilesController(IMemoryCacheFileSevice memoryCacheFileSevice, ILoggerService loggerService)
        {
            this.memoryCacheFileSevice = memoryCacheFileSevice;
            this.loggerService = loggerService;
        }

        [HttpDelete]
        [Route("DeleteAll")]
        public ActionResult<IEnumerable<RemoveAllFilesForSessionResourceModel>> RemoveAllFilesForSession(
            [FromQuery] RemoveAllFilesForSessionInputModel inputModel)
        {
            try
            {
                return this.memoryCacheFileSevice.RemoveAllFilesForSession<RemoveAllFilesForSessionResourceModel>(
                    inputModel);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<RemoveFileResourceModel> RemoveFile([FromRoute] [FromQuery] RemoveFileInputModel inputModel)
        {
            try
            {
                return this.memoryCacheFileSevice.RemoveFile<RemoveFileResourceModel>(inputModel);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<UploadFileResourceModel> UploadFile([FromForm] [FromQuery] UploadFileInputModel inputModel)
        {
            try
            {
                return this.memoryCacheFileSevice.UploadFile<UploadFileResourceModel>(inputModel);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }
    }
}