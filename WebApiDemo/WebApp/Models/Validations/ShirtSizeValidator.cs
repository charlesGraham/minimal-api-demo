using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Validations
{
    public class ShirtSizeValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            var shirt = validationContext.ObjectInstance as Shirt;

            if (shirt == null || string.IsNullOrEmpty(shirt.Gender))
            {
                return new ValidationResult("Shirt is required and gender is required");
            }

            if (shirt.Gender.Equals("Men", StringComparison.OrdinalIgnoreCase) && shirt.Size < 8)
            {
                return new ValidationResult("Men's shirts must be size 8 or larger");
            }
            else if (
                shirt.Gender.Equals("Women", StringComparison.OrdinalIgnoreCase)
                && shirt.Size < 6
            )
            {
                return new ValidationResult("Women's shirts must be size 6 or larger");
            }

            return ValidationResult.Success;
        }
    }
}
