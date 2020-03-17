﻿namespace VinylExchange.Data.Models
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Style> Styles { get; set; } = new HashSet<Style>();
    }
}