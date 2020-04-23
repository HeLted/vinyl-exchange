namespace VinylExchange.Models.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class LoginUserInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}