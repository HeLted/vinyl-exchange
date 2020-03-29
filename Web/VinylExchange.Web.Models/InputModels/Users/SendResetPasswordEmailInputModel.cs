namespace VinylExchange.Web.Models.InputModels.Users
{
    #region

    using System.ComponentModel.DataAnnotations;

    using static VinylExchange.Common.Constants.ValidationConstants;

    #endregion

    public class SendResetPasswordEmailInputModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(100, ErrorMessage = InvalidMaxLength)]
        public string Email { get; set; }
    }
}