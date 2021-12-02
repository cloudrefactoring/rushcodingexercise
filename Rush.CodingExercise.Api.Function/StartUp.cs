using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Rush.CodingExercise.Api.Function;
using Rush.CodingExercise.Data;
using Rush.CodingExercise.Messaging;
using Rush.CodingExercise.Service.Data;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Rush.CodingExercise.Api.Function;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddTransient<ISQLDataAdapter, SQLDataAdapter>();
        builder.Services.AddTransient<IOrderNumber, OrderNumber>();
        builder.Services.AddTransient<IOrderProcess, OrderProcess>();
        builder.Services.AddScoped<IServiceBus, ServiceBus>();
    }
}
