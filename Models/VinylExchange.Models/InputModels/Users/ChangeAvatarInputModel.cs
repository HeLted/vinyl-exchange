using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VinylExchange.Models.InputModels.Users
{
    public class ChangeAvatarInputModel
    {
        [Required]
        public IFormFile Avatar { get; set; }
    }
}
