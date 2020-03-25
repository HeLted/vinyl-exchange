using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VinylExchange.Common.Enumerations;

namespace VinylExchange.Web.Infrastructure.ValidationAttributes
{
    public class ValidateFileAttribute : ValidationAttribute

    {
        private readonly FileType? fileType;

        public ValidateFileAttribute(int fileTypeAsInt)
        {
            this.fileType = (FileType?)fileTypeAsInt;
        }

        public ValidateFileAttribute()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {
            int audioMaxContentSize = 40000000; //40 Mb

            int imageMaxContentSize = 10000000; // 10 Mb

            string[] allowedImageTypes = new string[] { ".jpg", ".jpeg", ".png" };

            string[] allowedAudioTypes = new string[] { ".mp3" };



            if (!(value is IFormFile file))
            {
               return new ValidationResult($"File is required");
            }
            else
            {

                if (allowedImageTypes.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.')))
                    && (fileType == null || fileType == FileType.Image))

                {

                    if (file.Length > imageMaxContentSize)

                    {

                        return new ValidationResult($"Your image is too large, maximum allowed size is : " + (imageMaxContentSize / 1024).ToString() + "MB");


                    }
                    else
                    {
                        return ValidationResult.Success;
                    }

                }
                else if (allowedAudioTypes.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.')))
                       && (fileType == null || fileType == FileType.Audio))
                {

                    if (file.Length > imageMaxContentSize)

                    {

                        return new ValidationResult("Your audio file is too large, maximum allowed size is : " + (audioMaxContentSize / 1024).ToString() + "MB");


                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
                else
                {

                    return new ValidationResult("Invalid upload format!");

                }


            }

        }

    }
}
