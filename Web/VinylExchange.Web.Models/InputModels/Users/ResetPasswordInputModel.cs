﻿namespace VinylExchange.Web.Models.InputModels.Users
{
    #region

    using System.ComponentModel.DataAnnotations;

    using static VinylExchange.Common.Constants.ValidationConstants;

    #endregion

    public class ResetPasswordInputModel
    {
        [Required]
        public string ResetPasswordToken { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100, ErrorMessage = InvalidMaxLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = InvalidMinLength)]
        [MaxLength(100, ErrorMessage = InvalidMaxLength)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = PassAndConfrimPassNotMatching)]
        public string ConfirmPassword { get; set; }
    }
}