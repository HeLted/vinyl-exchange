using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VinylExchange.Common.Enumerations;

namespace VinylExchange.Data.Common.Models
{
    public class BaseFile
    {
        public Guid Id { get; set; }
        [Required]
        public string Path { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public FileType FileType { get; set; }
        [Required]

        public DateTime CreatedOn { get; set; }
    }
}
