namespace VinylExchange.Web.Models.InputModels.Files
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    using VinylExchange.Web.ModelBinding.ValidationAttributes;

    public class UploadFileInputModel
    {
        [ValidateFile]
        public IFormFile File { get; set; }

        [Required]
        public Guid FormSessionId { get; set; }
    }
}