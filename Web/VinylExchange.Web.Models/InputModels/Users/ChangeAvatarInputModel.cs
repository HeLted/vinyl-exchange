namespace VinylExchange.Models.InputModels.Users
{
    #region

    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    #endregion

    public class ChangeAvatarInputModel
    {
        [Required]
        public IFormFile Avatar { get; set; }
    }
}