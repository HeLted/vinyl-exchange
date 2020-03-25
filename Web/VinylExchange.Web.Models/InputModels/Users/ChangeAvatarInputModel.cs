﻿namespace VinylExchange.Models.InputModels.Users
{
    #region

    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using VinylExchange.Common.Enumerations;
    using VinylExchange.Web.Infrastructure.ValidationAttributes;

    #endregion

    public class ChangeAvatarInputModel
    {
        [Required]
        [ValidateFile((int)FileType.Image)]
        public IFormFile Avatar { get; set; }
    }
}