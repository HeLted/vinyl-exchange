using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VinylExchange.Data.Models
{
    public class Style
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Genre Genre { get; set; }
        [Required]
        public int GenreId { get; set; }
        public ICollection<StyleRelease> Releases { get; set; } = new HashSet<StyleRelease>();
    }
}
