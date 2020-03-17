namespace VinylExchange.Models.InputModels.Users
{
    #region

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class LoginUserInputModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        [Required]

        public string Username { get; set; }
    }
}