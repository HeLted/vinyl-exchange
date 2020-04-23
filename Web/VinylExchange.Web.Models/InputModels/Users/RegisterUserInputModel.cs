namespace VinylExchange.Models.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Services.Mapping;
    using static Common.Constants.RegexPatterns;
    using static Common.Constants.ValidationConstants;


    public class RegisterUserInputModel : IMapTo<VinylExchangeUser>
    {
        [Required]
        [MinLength(3, ErrorMessage = InvalidMinLength)]
        [MaxLength(50, ErrorMessage = InvalidMaxLength)]
        [RegularExpression(AlphaNumericAndUnderscore, ErrorMessage = AllowedAplhaNumericAndUnderscore)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100, ErrorMessage = InvalidMaxLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = InvalidMinLength)]
        [MaxLength(100, ErrorMessage = InvalidMaxLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = PassAndConfrimPassNotMatching)]
        public string ConfirmPassword { get; set; }
    }
}