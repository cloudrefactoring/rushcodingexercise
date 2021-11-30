using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace Rush.CodingExercise.Api.Basic;

public class ServiceBus : IServiceBus
{
    private readonly ServiceBusClient _client;
    private readonly Azure.Messaging.ServiceBus.ServiceBusSender _clientSender;
    private const string QUEUE_NAME = "rushce";

    public ServiceBus(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ServiceBus");
        _client = new ServiceBusClient(connectionString);
        _clientSender = _client.CreateSender(QUEUE_NAME);
    }

    public async Task SendMessage(dynamic payload)
    {
        string messagePayload = JsonSerializer.Serialize(payload);
        ServiceBusMessage message = new ServiceBusMessage(messagePayload);
        await _clientSender.SendMessageAsync(message).ConfigureAwait(false);
    }
}
