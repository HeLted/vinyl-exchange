namespace VinylExchange.Data.Models
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Data.Common.Models;

    #endregion

    public class ReleaseFile : BaseDeletableModel
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public FileType FileType { get; set; }

        [Required]
        public bool IsPreview { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public Release Release { get; set; }

        [Required]
        public Guid? ReleaseId { get; set; }
    }
}