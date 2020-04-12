namespace VinylExchange.Web.Models.InputModels.Files
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using VinylExchange.Web.ModelBinding.ValidationAttributes;

    #endregion

    public class UploadFileInputModel
    {
        [ValidateFile]
        public IFormFile File { get; set; }

        [Required]
        public Guid? FormSessionId { get; set; }
    }
}