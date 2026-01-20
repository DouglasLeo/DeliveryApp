using DeliveryApp.API.Extensions;
using DeliveryApp.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();

builder.Services.AddInfrastructure();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();