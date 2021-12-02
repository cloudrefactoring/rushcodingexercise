
namespace Rush.CodingExercise.Messaging;

public interface IServiceBus
{
    Task SendMessage(dynamic payload);
}