namespace VinylExchange.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Models;

    public class Release : BaseDeletableModel
    {
        [Required]
        public string Artist { get; set; }

        [Required]
        public string Format { get; set; }

        [Required]
        public string Label { get; set; }

        public ICollection<CollectionItem> ReleaseCollections { get; set; } = new HashSet<CollectionItem>();

        public ICollection<ReleaseFile> ReleaseFiles { get; set; } = new HashSet<ReleaseFile>();

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();

        public ICollection<StyleRelease> Styles { get; set; } = new HashSet<StyleRelease>();

        [Required]
        public string Title { get; set; }

        [Required]
        public string Year { get; set; }
    }
}