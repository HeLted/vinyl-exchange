namespace VinylExchange.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Style
    {
        public Genre Genre { get; set; }

        [Required]
        public int GenreId { get; set; }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<StyleRelease> Releases { get; set; } = new HashSet<StyleRelease>();
    }
}