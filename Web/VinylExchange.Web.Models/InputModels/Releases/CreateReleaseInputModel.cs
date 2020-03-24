namespace VinylExchange.Web.Models.InputModels.Releases
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VinylExchange.Common.Constants;
    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;
    using VinylExchange.Web.Infrastructure.ValidationAttributes;

    #endregion

    public class CreateReleaseInputModel : IMapTo<Release>
    {
        [Required]
        [MinLength(3,ErrorMessage ="Invalid min length of field!")]
        [MaxLength(40,ErrorMessage ="Invalid max length of field!")]
        [RegularExpression(RegexPatterns.AplhaNumericBracesAndDash,ErrorMessage ="Allowed characters are (A-Z,a-z,0-9,(,),-,whitespace)!")]
        public string Artist { get; set; }

        [Required]
        [MinLength(2,ErrorMessage ="Invalid min length of field!")]
        [MaxLength(15,ErrorMessage ="Invalid max length of field!")]
        [RegularExpression(RegexPatterns.AplhaNumericBracesAndDash,ErrorMessage ="Allowed characters are (A-Z,a-z,0-9,(,),-,whitespace)!")]
        public string Format { get; set; }

        [Required]
        [MinLength(3,ErrorMessage ="Invalid min length of field!")]
        [MaxLength(30,ErrorMessage ="Invalid max length of field!")]
        [RegularExpression(RegexPatterns.AplhaNumericBracesAndDash,ErrorMessage ="Allowed characters are (A-Z,a-z,0-9,(,),-,whitespace)!")]
        public string Label { get; set; }

        public ICollection<int> StyleIds { get; set; } = new HashSet<int>();

        [Required]
        [MinLength(3,ErrorMessage ="Invalid min length of field")]
        [MaxLength(40,ErrorMessage ="Invalid max length of field")]
        [RegularExpression(RegexPatterns.AplhaNumericBracesAndDash,ErrorMessage ="Allowed characters are (A-Z,a-z,0-9,(,),-,whitespace)!")]
        public string Title { get; set; }

        [Required]
        [ValidateYear]
        public int Year { get; set; }
    }
}