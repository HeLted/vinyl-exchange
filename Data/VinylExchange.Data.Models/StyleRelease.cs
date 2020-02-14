using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VinylExchange.Data.Models
{
    public class StyleRelease
    {
        [Required]
        public int StyleId { get; set; }
        public Style Style { get; set; }
        [Required]
        public Guid ReleaseId { get; set; }
        public Release Release { get; set; }
    }
}
