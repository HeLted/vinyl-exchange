namespace VinylExchange.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class StyleRelease
    {
        public Release Release { get; set; }

        [Required] public Guid? ReleaseId { get; set; }

        public Style Style { get; set; }

        [Required] public int StyleId { get; set; }
    }
}