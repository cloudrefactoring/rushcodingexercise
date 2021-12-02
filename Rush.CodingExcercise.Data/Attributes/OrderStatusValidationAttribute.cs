using Rush.CodingExercise.Data.Model.Enum;

using System.ComponentModel.DataAnnotations;

namespace Rush.CodingExercise.Data;

public class OrderStatusValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value,
    ValidationContext validationContext)
    {
        if (!Enum.IsDefined(typeof(OrderStatus), value))
        {
            return new ValidationResult("Invalid Status Value. Acceptable values are: Pending,Ordered,Billed,Shipped,Delivered.");
        }

        return ValidationResult.Success;
    }
}