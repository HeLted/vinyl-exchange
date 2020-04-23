namespace VinylExchange.Web.Models.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using static Common.Constants.ValidationConstants;


    public class SendResetPasswordEmailInputModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(100, ErrorMessage = InvalidMaxLength)]
        public string Email { get; set; }
    }
}