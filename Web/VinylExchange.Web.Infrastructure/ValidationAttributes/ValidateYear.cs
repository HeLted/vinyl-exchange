using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VinylExchange.Web.Infrastructure.ValidationAttributes
{
    public class ValidateYear: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {           
            var currentYear = DateTime.UtcNow.Year;
            
            if ((int)value >= 1930 &&  (int)value <= currentYear)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"Year must be in range between 1930 and {currentYear}");
            }
        }
    }
}
