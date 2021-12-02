using System.ComponentModel.DataAnnotations;

namespace Rush.CodingExercise.Data.Model.DTO;

[UpdateOrderModelValidator]
public class UpdateOrder
{
    [OrderNumberValidation]
    public string? OrderNumber { get; set; }

    public decimal Total { get; set; }

    [OrderStatusValidation]
    public string Status { get; set; } = default!;
}

