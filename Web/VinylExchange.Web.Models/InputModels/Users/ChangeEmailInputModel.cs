namespace VinylExchange.Web.Models.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using static Common.Constants.ValidationConstants;


    public class ChangeEmailInputModel
    {
        [Required]
        public string ChangeEmailToken { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100, ErrorMessage = InvalidMaxLength)]
        public string NewEmail { get; set; }
    }
}