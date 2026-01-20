using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeliveryApp.Infrastructure.Persistence.Mongo.MongoConfigurations;

public sealed class MongoInitializerHostedService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var initializer = scope.ServiceProvider
            .GetRequiredService<MongoInitializer>();

        await initializer.InitializeAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}