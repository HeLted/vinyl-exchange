namespace VinylExchange.Web.Models.Utility
{
    using System;
    using System.IO;

    using Microsoft.AspNetCore.Http;

    using VinylExchange.Common.Constants;
    using VinylExchange.Common.Enumerations;

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
            using MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }
    }
}