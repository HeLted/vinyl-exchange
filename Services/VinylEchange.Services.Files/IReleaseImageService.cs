using Microsoft.AspNetCore.Http;
using System;

namespace VinylEchange.Services.Files
{
    public interface IReleaseImageService
    {
        string GetImagePath(Guid releaseEntityId);
        bool SaveImage(Guid releaseEntityId, IFormFile formFile);
    }
}
