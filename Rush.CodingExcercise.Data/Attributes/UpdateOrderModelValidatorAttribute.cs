using Rush.CodingExercise.Data.Model.DTO;

namespace Rush.CodingExercise.Data;

[AttributeUsage(AttributeTargets.Class|AttributeTargets.Class,AllowMultiple = true)]
public class UpdateOrderModelValidatorAttribute : Attribute
{
    public UpdateOrderModelValidatorAttribute()
    {
    }

    public override bool Match(object? obj)
    {
        if (obj is not UpdateOrder) return false;

        return true;
    }
}