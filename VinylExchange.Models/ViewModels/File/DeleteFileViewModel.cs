using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Models.ViewModels.File
{
   public  class DeleteFileViewModel
    {
        public Guid FileId { get; set; }

        public string FileName { get; set; }
    }
}
