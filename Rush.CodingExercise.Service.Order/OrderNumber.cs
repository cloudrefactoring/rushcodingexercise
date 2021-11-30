namespace Rush.CodingExercise.Service.Data;

public class OrderNumber : IOrderNumber
{
    public string GetNext()
    {
        var orderSequence = GenerateOrderSequenceNumber().ToString("000000");
        return $"ORD{orderSequence}";
    }
    private static int GenerateOrderSequenceNumber()
    {
        var rand = new Random();

        var randomNumber = rand.Next(0, 1000000);
        return randomNumber;
    }
}
