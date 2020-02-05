using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Models.Utility;
using VinylExchange.Services.MemoryCache;
using System.IO;

namespace VinylExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly MemoryCacheManager cacheManager;

        public FileController(MemoryCacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteFile(string formSessionId, string fileGuid)
        {

            var key = cacheManager.GetKeys().Where(x => x == formSessionId).SingleOrDefault();

            var formSessionStorage = cacheManager.Get<List<UploadFileUtilityModel>>(key, null);

            var image = formSessionStorage.SingleOrDefault(x => x.FileGuid.ToString() == fileGuid);

            if (image != null) {
                formSessionStorage.Remove(image);

                var returnObj = new
                {
                    removed = image.FileName,
                    filesStillInStorage = string.Join(",", formSessionStorage.Select(x => x.FileName))
                };

                return Json(returnObj);
            }
            else
            {
                return BadRequest("there is no file with this fileGuid in cache storage");
            }

        }

        [HttpPost]
        [Route("upload")]
        public IActionResult UploadFile(IFormFile file, string formSessionId)
        {
            var imageGuid = Guid.NewGuid();
                       
            UploadFileUtilityModel image = new UploadFileUtilityModel(file, imageGuid);

            if (!cacheManager.IsSet(formSessionId))
            {
                cacheManager.Set(formSessionId, new List<UploadFileUtilityModel>(), 1800);
            }

            var formSessionStorage = cacheManager.Get<List<UploadFileUtilityModel>>(formSessionId, null);

            formSessionStorage.Add(image);

            var returnObj = new
            {
                added = image.FileName,
                guid = imageGuid,
                filesInStorage = string.Join(",", formSessionStorage.Select(x => x.FileName))
            };

            return Json(returnObj);
        }


    }
}
