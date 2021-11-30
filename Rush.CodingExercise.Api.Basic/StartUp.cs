using Rush.CodingExercise.Data;
using Rush.CodingExercise.Service.Data;
using Rush.CodingExercise.Api.Basic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISQLDataAdapter, SQLDataAdapter>();
builder.Services.AddTransient<IOrderNumber, OrderNumber>();
builder.Services.AddTransient<IOrderProcess, OrderProcess>();
builder.Services.AddScoped<IServiceBus, ServiceBus>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
