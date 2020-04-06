namespace VinylExchange.Data.Models
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class StyleRelease
    {
        public Release Release { get; set; }

        [Required]
        public Guid? ReleaseId { get; set; }

        public Style Style { get; set; }

        [Required]
        public int StyleId { get; set; }
    }
}