using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Tools;

namespace VinylExchange.Controllers
{
    [Route("file/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly MemoryCacheManager cache;

        public ImagesController(MemoryCacheManager cache)
        {
            this.cache = cache;
        }


        [HttpPost]
        [Route("upload")]
        public IActionResult UploadImage(IFormFile file, string formSessionId)
        {
            string cacheGuid = (Guid.NewGuid()).ToString();

            cache.Set( cacheGuid + "-" + formSessionId + "-" + file.FileName, file, 1000);

            return View("<h1>Heii</h1>");
        }


    }
}
