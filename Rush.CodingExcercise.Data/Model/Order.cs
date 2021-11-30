namespace Rush.CodingExercise.Data.Model;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? OrderNumber { get; set; }
    public decimal? Total { get; set; }
    public string? Status { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
}