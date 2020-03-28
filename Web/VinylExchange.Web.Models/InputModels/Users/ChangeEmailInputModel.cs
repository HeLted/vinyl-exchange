namespace VinylExchange.Web.Models.InputModels.Users
{
    #region

    using System.ComponentModel.DataAnnotations;

    using static VinylExchange.Common.Constants.ValidationConstants;

    #endregion

    public class ChangeEmailInputModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(100, ErrorMessage = InvalidMaxLength)]
        public string NewEmail { get; set; }

        [Required]
        public string ChangeEmailToken { get; set; }
    }
}