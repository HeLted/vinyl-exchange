namespace VinylExchange.Data.Models
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Common.Models;
    using static VinylExchange.Common.Constants.RegexPatterns;

    #endregion

    public class Address : BaseModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression(LettersOnly)]
        public string Country { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression(LettersOnly)]
        public string Town { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression(AplhaNumericBracesDashAndSpace)]
        public string PostalCode { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        [RegularExpression(AlphaNumericDotCommaAndSpace)]

        public string FullAddress { get; set; }

        public VinylExchangeUser User { get; set; }

        [Required]
        public Guid? UserId { get; set; }
    }
}