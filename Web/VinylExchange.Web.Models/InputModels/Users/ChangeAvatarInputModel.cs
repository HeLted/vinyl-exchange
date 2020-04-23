namespace VinylExchange.Models.InputModels.Users
{
    using Common.Enumerations;
    using Microsoft.AspNetCore.Http;
    using Web.ModelBinding.ValidationAttributes;

    public class ChangeAvatarInputModel
    {
        [ValidateFile(FileType.Image)] public IFormFile Avatar { get; set; }
    }
}