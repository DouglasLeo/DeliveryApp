using DeliveryApp.API;
using DeliveryApp.API.Extensions;
using DeliveryApp.Application.Shared;
using DeliveryApp.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.AddWebServices();

var app = builder.Build();

app.UseExceptionHandler(options => { });
app.MapGet("/", () => "Hello World!");
app.MapEndpoints();

app.MapOpenApi();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "DeliveryApp.API"));

app.Run();