namespace VinylExchange.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Models.InputModels.Files;
    using Models.ResourceModels.File;
    using Models.ResourceModels.Files;
    using Services.Logging;
    using Services.MemoryCache.Contracts;

    public class FilesController : ApiController
    {
        private readonly ILoggerService loggerService;

        private readonly IMemoryCacheFilesSevice memoryCacheFileSevice;

        public FilesController(IMemoryCacheFilesSevice memoryCacheFileSevice, ILoggerService loggerService)
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
                return memoryCacheFileSevice.RemoveAllFilesForSession<RemoveAllFilesForSessionResourceModel>(
                    inputModel);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);

                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<RemoveFileResourceModel> RemoveFile([FromRoute] [FromQuery] RemoveFileInputModel inputModel)
        {
            try
            {
                return memoryCacheFileSevice.RemoveFile<RemoveFileResourceModel>(inputModel);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);

                return BadRequest();
            }
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public ActionResult<UploadFileResourceModel> UploadFile([FromQuery] [FromForm] UploadFileInputModel inputModel)
        {
            try
            {
                return memoryCacheFileSevice.UploadFile<UploadFileResourceModel>(
                    inputModel.File,
                    inputModel.FormSessionId);
            }
            catch (Exception ex)
            {
                loggerService.LogException(ex);

                return BadRequest();
            }
        }
    }
}