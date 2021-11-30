namespace Rush.CodingExercise.Data.Model.DTO
{
    public class UpdateOrder
    {
        public string? OrderNumber { get; set; }
        public decimal Total { get; set; }
        public string? Status { get; set; }
    }
}