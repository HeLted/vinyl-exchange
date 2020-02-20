using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Models.ResourceModels.File
{
   public  class DeleteFileResourceModel
    {
        public Guid FileId { get; set; }

        public string FileName { get; set; }
    }
}
