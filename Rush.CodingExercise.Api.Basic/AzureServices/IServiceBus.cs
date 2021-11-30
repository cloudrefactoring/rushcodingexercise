
namespace Rush.CodingExercise.Api.Basic;

public interface IServiceBus
{
    Task SendMessage(dynamic payload);
}