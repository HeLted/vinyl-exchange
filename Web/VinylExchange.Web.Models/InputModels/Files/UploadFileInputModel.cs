using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using VinylExchange.Web.Infrastructure.ValidationAttributes;

namespace VinylExchange.Web.Models.InputModels.Files
{
    public class UploadFileInputModel
    {
        [ValidateFile]
        public IFormFile File { get; set; }

        [Required]
        public Guid FormSessionId { get; set; }
    }
}
