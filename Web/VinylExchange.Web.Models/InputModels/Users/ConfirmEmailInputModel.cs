namespace VinylExchange.Models.InputModels.Users
{
    #region

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class ConfirmEmailInputModel
    {
        [Required]
        public string EmailConfirmToken { get; set; }
    }
}