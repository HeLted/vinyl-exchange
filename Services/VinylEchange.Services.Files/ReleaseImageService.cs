using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace VinylEchange.Services.Files
{
    public class ReleaseImageService : IReleaseImageService
    {
        private const string staticDirectoryPath = @"wwwroot\releaseImages\";
        private const string htmlSourceDirectoryPath = @"\releaseImages\";
        private const string imageFileExtension = ".jpg";
        public string GetImagePath(Guid releaseEntityId)
        {
            return  htmlSourceDirectoryPath + releaseEntityId.ToString().ToLower() + imageFileExtension;
        }
               
        public bool SaveImage(Guid releaseEntityId, IFormFile formFile)
        {
            using var file = File.OpenWrite(staticDirectoryPath + releaseEntityId +  imageFileExtension);
            formFile.CopyTo(file);

            return File.Exists(staticDirectoryPath + releaseEntityId + imageFileExtension);
        }       
    }
}
