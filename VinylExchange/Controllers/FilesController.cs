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

        [HttpDelete]
        [Route("{id}")]
      
        public IActionResult DeleteFile(Guid id, Guid formSessionId)
        {

            var returnObj = this.memoryCacheFileSevice.RemoveFile(formSessionId, id);

            return Ok(returnObj);

        }

        [HttpDelete]
        [Route("DeleteAll")]
        public IActionResult DeleteAllFiles(Guid formSessionId)
        {
            this.memoryCacheFileSevice.RemoveAllFilesForFormSession(formSessionId);

            return Ok();
            
        }


        [HttpPost]        
        public IActionResult UploadFile(IFormFile file, Guid formSessionId)
        {
           
            UploadFileUtilityModel fileModel = new UploadFileUtilityModel(file);

            var returnObj = this.memoryCacheFileSevice.AddFile(fileModel, formSessionId);

            return Ok(returnObj);
        }


    }
}
