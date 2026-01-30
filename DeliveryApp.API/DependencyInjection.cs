using System.Text.Json.Serialization;
using DeliveryApp.API.Infrastructure;

namespace DeliveryApp.API;

public static class DependencyInjection
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }
}