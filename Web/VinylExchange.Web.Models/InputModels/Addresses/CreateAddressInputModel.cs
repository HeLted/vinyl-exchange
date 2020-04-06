namespace VinylExchange.Web.Models.InputModels.Addresses
{
    #region

    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    using static VinylExchange.Common.Constants.RegexPatterns;
    using static VinylExchange.Common.Constants.ValidationConstants;

    #endregion

    public class CreateAddressInputModel : IMapTo<Address>
    {
        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(40, ErrorMessage = InvalidMaxLength)]
        [RegularExpression(LettersOnly, ErrorMessage = AllowedLettersOnly)]

        public string Country { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(40, ErrorMessage = InvalidMaxLength)]
        [RegularExpression(LettersOnly, ErrorMessage = AllowedLettersOnly)]

        public string Town { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(40, ErrorMessage = InvalidMaxLength)]
        [RegularExpression(AplhaNumericBracesDashAndSpace, ErrorMessage = AllowedAplhaNumericBracesDashAndSpace)]

        public string PostalCode { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(300, ErrorMessage = InvalidMaxLength)]
        [RegularExpression(AlphaNumericDotCommaAndSpace, ErrorMessage = AllowedAplhaNumericDotCommaAndSpace)]

        public string FullAddress { get; set; }
    }
}