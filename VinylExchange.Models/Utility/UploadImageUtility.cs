using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Models.Utility
{
    public class UploadImageUtility
    {

        public UploadImageUtility(IFormFile file,Guid fileGuid)
        {
            this.File = file;            
            this.DateTime = DateTime.UtcNow;
            this.FileGuid = fileGuid;
        }
        public IFormFile File { get; set; }
           
        public  DateTime DateTime { get; set; }

        public  Guid FileGuid { get; set; }

    }
}
