namespace VinylExchange.Data.Models
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Models;

    using static VinylExchange.Common.Constants.RegexPatterns;

    #endregion

    public class Release : BaseDeletableModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression(AplhaNumericBracesDashAndSpace)]
        public string Artist { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression(AplhaNumericBracesDashAndSpace)]
        public string Title { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        [RegularExpression(AplhaNumericBracesDashAndSpace)]
        public string Format { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [RegularExpression(AplhaNumericBracesDashAndSpace)]
        public string Label { get; set; }

        public ICollection<CollectionItem> ReleaseCollections { get; set; } = new HashSet<CollectionItem>();

        public ICollection<ReleaseFile> ReleaseFiles { get; set; } = new HashSet<ReleaseFile>();

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();

        public ICollection<StyleRelease> Styles { get; set; } = new HashSet<StyleRelease>();
    }
}