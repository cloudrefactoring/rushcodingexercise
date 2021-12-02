using System.ComponentModel.DataAnnotations;

namespace Rush.CodingExercise.Data;

public class OrderNumberValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value,
    ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty((string?)value))
        {
            return new ValidationResult("Order number is required.");
        }

        return ValidationResult.Success;
    }
}