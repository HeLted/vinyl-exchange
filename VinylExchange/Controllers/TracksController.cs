using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using VinylExchange.Services.MemoryCache;

namespace VinylExchange.Controllers
{
    public class TracksController : ApiController
    {
        private readonly MemoryCacheManager cache;

        public TracksController(MemoryCacheManager cache)
        {
            this.cache = cache;
        }


        [HttpPost]
        [Route("Upload")]
        public IActionResult UploadTrack(IFormFile file, string formSessionId)
        {

            string cacheGuid = (Guid.NewGuid()).ToString();

            cache.Set(cacheGuid + "-" + formSessionId + "-" + file.FileName, file, 1000);
                                 
            return Ok($"{string.Join(Environment.NewLine, cache.GetKeys())}");
        }


    }
}
