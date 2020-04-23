namespace VinylExchange.Web.Models.InputModels.Releases
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using ModelBinding.ValidationAttributes;
    using Services.Mapping;
    using static Common.Constants.RegexPatterns;
    using static Common.Constants.ValidationConstants;


    public class CreateReleaseInputModel : IMapTo<Release>
    {
        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(40, ErrorMessage = InvalidMaxLength)]
        [RegularExpression(AplhaNumericBracesDashAndSpace, ErrorMessage = AllowedAplhaNumericBracesDashAndSpace)]
        public string Artist { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(40, ErrorMessage = InvalidMaxLength)]
        [RegularExpression(AplhaNumericBracesDashAndSpace, ErrorMessage = AllowedAplhaNumericBracesDashAndSpace)]
        public string Title { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = InvalidMinLength)]
        [MaxLength(15, ErrorMessage = InvalidMaxLength)]
        [RegularExpression(AplhaNumericBracesDashAndSpace, ErrorMessage = AllowedAplhaNumericBracesDashAndSpace)]
        public string Format { get; set; }

        [Required]
        [ValidateYear]
        public int Year { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(30, ErrorMessage = InvalidMaxLength)]
        [RegularExpression(AplhaNumericBracesDashAndSpace, ErrorMessage = AllowedAplhaNumericBracesDashAndSpace)]
        public string Label { get; set; }

        public ICollection<int> StyleIds { get; set; } = new HashSet<int>();
    }
}