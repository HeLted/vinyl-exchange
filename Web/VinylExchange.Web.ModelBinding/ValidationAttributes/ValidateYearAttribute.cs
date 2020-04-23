namespace VinylExchange.Web.ModelBinding.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ValidateYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentYear = DateTime.UtcNow.Year;

            if ((int) value >= 1930 &&
                (int) value <= currentYear)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Year must be in range between 1930 and {currentYear}");
        }
    }
}