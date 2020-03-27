namespace VinylExchange.Web.ModelBinding.ValidationAttributes
{
    #region

    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    using VinylExchange.Common.Enumerations;

    #endregion

    public class ValidateFileAttribute : ValidationAttribute
    {
        private readonly FileType? fileType;

        public ValidateFileAttribute(FileType fileType)
        {
            this.fileType = fileType;
        }

        public ValidateFileAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var audioMaxContentSize = 41943040; // 40 Mb

            var imageMaxContentSize = 10485760; // 10 Mb

            string[] allowedImageTypes = { ".jpg", ".jpeg", ".png" };

            string[] allowedAudioTypes = { ".mp3" };

            if (!(value is IFormFile file))
            {
                return new ValidationResult("Please upload file into the field!");
            }

            if (allowedImageTypes.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.')))
                && (this.fileType == null || this.fileType == FileType.Image))
            {
                if (file.Length > imageMaxContentSize)
                {
                    return new ValidationResult(
                        "Your image is too large, maximum allowed size is : " + imageMaxContentSize / 1024f / 1024f
                                                                              + "MB");
                }

                return ValidationResult.Success;
            }

            if (allowedAudioTypes.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.')))
                && (this.fileType == null || this.fileType == FileType.Audio))
            {
                if (file.Length > audioMaxContentSize)
                {
                    return new ValidationResult(
                        "Your audio file is too large, maximum allowed size is : " + audioMaxContentSize / 1024f / 1024f
                                                                                   + "MB");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid upload format!");
        }
    }
}