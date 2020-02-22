using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VinylExchange.Common.Enumerations;
using VinylExchange.Data.Common.Models;

namespace VinylExchange.Data.Models
{
    public class ReleaseFile : BaseFile
    {
        public ReleaseFile()
        {
            this.Id = Guid.NewGuid();
        }
       
        public Release Release { get; set; }

        [Required]
        public Guid ReleaseId { get; set; }

    }
}
