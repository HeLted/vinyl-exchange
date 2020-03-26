namespace VinylExchange.Web.Models.InputModels.Files
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using VinylExchange.Web.ModelBinding.ValidationAttributes;

    public class UploadFileInputModel
    {
        [ValidateFile]
        public IFormFile File { get; set; }

        [Required]
        public Guid FormSessionId { get; set; }
    }
}