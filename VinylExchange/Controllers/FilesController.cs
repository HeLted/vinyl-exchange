using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using VinylExchange.Models.Utility;
using VinylExchange.Services.MemoryCache;

namespace VinylExchange.Controllers
{
    public class FilesController : ApiController
    {
        private readonly IMemoryCacheFileSevice memoryCacheFileSevice;

        public FilesController(IMemoryCacheFileSevice memoryCacheFileSevice)
        {
            this.memoryCacheFileSevice = memoryCacheFileSevice;
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteFile(string formSessionId, string fileGuid)
        {

            var returnObj = this.memoryCacheFileSevice.RemoveFile(formSessionId, Guid.Parse(fileGuid));

            return Ok(returnObj);

        }

        [HttpPost]
        [Route("DeleteAll")]
        public IActionResult DeleteAllFiles(string formSessionId)
        {

            this.memoryCacheFileSevice.RemoveAllFilesForFormSession(formSessionId);

            return Ok();


        }


        [HttpPost]
        [Route("Upload")]
        public IActionResult UploadFile(IFormFile file, string formSessionId)
        {
           
            UploadFileUtilityModel fileModel = new UploadFileUtilityModel(file);

            var returnObj = this.memoryCacheFileSevice.AddFile(fileModel, formSessionId);

            return Ok(returnObj);
        }


    }
}
