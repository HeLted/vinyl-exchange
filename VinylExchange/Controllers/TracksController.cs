using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylExchange.Tools;

namespace VinylExchange.Controllers
{
    [Route("file/[controller]")]
    [ApiController]
    public class TracksController : Controller
    {
        private readonly MemoryCacheManager cache;

        public TracksController(MemoryCacheManager cache)
        {
            this.cache = cache;
        }


        [HttpPost]
        [Route("upload")]
        public IActionResult UploadTrack(IFormFile file, string formSessionId)
        {

            string cacheGuid = (Guid.NewGuid()).ToString();

            cache.Set(cacheGuid + "-" + formSessionId + "-" + file.FileName, file, 1000);




            return Ok($"{string.Join(Environment.NewLine, cache.GetKeys())}");
        }


    }
}
