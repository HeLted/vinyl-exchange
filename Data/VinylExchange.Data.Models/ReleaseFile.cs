namespace VinylExchange.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using VinylExchange.Common.Enumerations;

    public class ReleaseFile : BaseDeletableModel
    {
        [Required] public string FileName { get; set; }

        [Required] public FileType FileType { get; set; }

        [Required] public bool IsPreview { get; set; }

        [Required] public string Path { get; set; }

        [Required] public Release Release { get; set; }

        [Required] public Guid? ReleaseId { get; set; }
    }
}