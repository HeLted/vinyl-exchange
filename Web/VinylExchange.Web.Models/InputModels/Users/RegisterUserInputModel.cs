namespace VinylExchange.Models.InputModels.Users
{
    #region

    using System.ComponentModel.DataAnnotations;

    using VinylExchange.Data.Models;
    using VinylExchange.Services.Mapping;

    #endregion

    public class RegisterUserInputModel : IMapTo<VinylExchangeUser>
    {
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(
            100,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }
    }
}