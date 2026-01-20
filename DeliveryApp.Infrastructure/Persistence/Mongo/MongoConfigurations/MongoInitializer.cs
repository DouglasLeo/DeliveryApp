using DeliveryApp.Infrastructure.Persistence.Mongo.Abstractions;

namespace DeliveryApp.Infrastructure.Persistence.Mongo.MongoConfigurations;

public sealed class MongoInitializer(IEnumerable<IMongoCollectionInitializer> initializers)
{
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        foreach (var initializer in initializers)
            await initializer.InitializeAsync(cancellationToken);
    }
}