using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using VinylExchange.Common.Enumerations;

namespace VinylExchange.Models.Utility
{
    public class UploadFileUtilityModel
    {

        public UploadFileUtilityModel(IFormFile file)
        {

            this.FileExtension =  file.FileName.Substring(file.FileName.LastIndexOf("."));
            this.FileName = file.FileName.Replace(this.FileExtension, String.Empty);
            this.FileType = this.FileExtension == ".mp3" ? FileType.Audio : FileType.Image;
            this.FileByteContent = this.ConvertIFormFileToByteArray(file);            
            this.CreatedOn = DateTime.UtcNow;
            this.FileGuid = new Guid();
        }
        public string FileName { get; set; }        
        public string FileExtension { get; set; }
        public FileType FileType { get; set; }
        public byte[] FileByteContent { get; set; }           
        public  DateTime CreatedOn { get; set; }
        public  Guid FileGuid { get; set; }      
        
        private byte[] ConvertIFormFileToByteArray(IFormFile file) {
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
            
        }
    }
}
