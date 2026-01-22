using System.Text.Json.Serialization;
using DeliveryApp.API.Extensions;
using DeliveryApp.Application.Shared;
using DeliveryApp.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddOpenApi();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapEndpoints();

app.MapOpenApi();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "DeliveryApp.API"));

app.Run();