namespace VinylExchange.Controllers
{
    using System;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using VinylExchange.Models.ResourceModels.File;
    using VinylExchange.Models.Utility;
    using VinylExchange.Services.Logging;
    using VinylExchange.Services.MemoryCache;

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
        public IActionResult DeleteAllFiles(Guid formSessionId)
        {
            try
            {
                this.memoryCacheFileSevice.RemoveAllFilesForFormSession(formSessionId);

                return this.Ok();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteFile(Guid id, Guid formSessionId)
        {
            try
            {
                DeleteFileResourceModel deletedFileResourceModel =
                    this.memoryCacheFileSevice.RemoveFile(formSessionId, id);

                return this.Ok(deletedFileResourceModel);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file, Guid formSessionId)
        {
            try
            {
                UploadFileUtilityModel fileModel = new UploadFileUtilityModel(file);

                UploadFileResourceModel uploadedFileResourceModel =
                    this.memoryCacheFileSevice.AddFile(fileModel, formSessionId);

                return this.Ok(uploadedFileResourceModel);
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);

                return this.BadRequest();
            }
        }
    }
}