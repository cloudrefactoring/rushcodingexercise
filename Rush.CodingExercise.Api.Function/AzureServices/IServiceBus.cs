
using System.Threading.Tasks;

namespace Rush.CodingExercise.Api.Function;

public interface IServiceBus
{
    Task SendMessage(dynamic payload);
}