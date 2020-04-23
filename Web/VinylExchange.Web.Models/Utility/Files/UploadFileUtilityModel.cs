namespace VinylExchange.Web.Models.Utility.Files
{
    using System;
    using System.IO;
    using Common.Constants;
    using Common.Enumerations;
    using Microsoft.AspNetCore.Http;

    public class UploadFileUtilityModel
    {
        public UploadFileUtilityModel(IFormFile file)
        {
            this.FileExtension = file.FileName.Substring(file.FileName.LastIndexOf("."));
            this.FileName = file.FileName.Replace(this.FileExtension, string.Empty);
            this.FileType = this.FileExtension == FileExtensionConstants.Mp3 ? FileType.Audio : FileType.Image;
            this.FileByteContent = this.ConvertIFormFileToByteArray(file);
            this.CreatedOn = DateTime.UtcNow;
            this.FileGuid = Guid.NewGuid();
        }

        public DateTime CreatedOn { get; set; }

        public byte[] FileByteContent { get; set; }

        public string FileExtension { get; set; }

        public Guid FileGuid { get; set; }

        public string FileName { get; set; }

        public FileType FileType { get; set; }

        private byte[] ConvertIFormFileToByteArray(IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }
    }
}