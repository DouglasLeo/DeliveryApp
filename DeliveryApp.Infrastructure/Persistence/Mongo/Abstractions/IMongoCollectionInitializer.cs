namespace DeliveryApp.Infrastructure.Persistence.Mongo.Abstractions;

public interface IMongoCollectionInitializer
{
    Task InitializeAsync(CancellationToken cancellationToken);
}