using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace VinylExchange.Models.Utility
{
    public class UploadFileUtilityModel
    {

        public UploadFileUtilityModel(IFormFile file,Guid fileGuid)
        {
            this.FileName = file.FileName;            
            this.FileByteContent = this.ConvertIFormFileToByteArray(file);            
            this.DateTime = DateTime.UtcNow;
            this.FileGuid = fileGuid;
        }

        public string FileName { get; set; }


        public byte[] FileByteContent { get; set; }
           
        public  DateTime DateTime { get; set; }

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
