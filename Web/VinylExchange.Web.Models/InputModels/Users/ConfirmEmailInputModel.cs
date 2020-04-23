namespace VinylExchange.Models.InputModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class ConfirmEmailInputModel
    {
        [Required] public string EmailConfirmToken { get; set; }
    }
}