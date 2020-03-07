﻿namespace VinylExchange.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Style> Styles { get; set; } = new HashSet<Style>();
    }
}