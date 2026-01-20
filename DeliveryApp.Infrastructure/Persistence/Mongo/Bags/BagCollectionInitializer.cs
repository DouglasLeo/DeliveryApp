using DeliveryApp.Domain.Entities.Bag;
using DeliveryApp.Infrastructure.Persistence.Mongo.Abstractions;
using MongoDB.Driver;

namespace DeliveryApp.Infrastructure.Persistence.Mongo.Bags;

public sealed class BagCollectionInitializer 
    : IMongoCollectionInitializer
{
    private readonly MongoDbContext _context;

    public BagCollectionInitializer(MongoDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await EnsureCollectionExists(cancellationToken);
        await EnsureIndexes(cancellationToken);
    }

    private async Task EnsureCollectionExists(CancellationToken cancellationToken)
    {
        var collections = await _context.Database
            .ListCollectionNames()
            .ToListAsync(cancellationToken);

        if (!collections.Contains("Bags"))
        {
            await _context.Database
                .CreateCollectionAsync("Bags", cancellationToken: cancellationToken);
        }
    }

    private async Task EnsureIndexes(CancellationToken cancellationToken)
    {
        var index = new CreateIndexModel<Bag>(
            Builders<Bag>.IndexKeys.Ascending(x => x.UserId),
            new CreateIndexOptions
            {
                Unique = true,
                Name = "IX_Bags_UserId_Unique"
            });

        await _context.Bags.Indexes
            .CreateOneAsync(index, cancellationToken: cancellationToken);
    }
}
