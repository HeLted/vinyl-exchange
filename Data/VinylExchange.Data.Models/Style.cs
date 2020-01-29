using System;
using System.Collections.Generic;
using System.Text;

namespace VinylExchange.Data.Models
{
    public class Style
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public ICollection<StyleRelease> Releases { get; set; } = new HashSet<StyleRelease>();
    }
}
