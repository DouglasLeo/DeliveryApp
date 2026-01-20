using DeliveryApp.Application.Bags.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Bag;
using MongoDB.Driver;

namespace DeliveryApp.Infrastructure.Persistence.Mongo.Bags.Repositories;

public class BagRepository : IBagRepository
{
    private readonly IMongoCollection<Bag> _bags;

    public BagRepository(MongoDbContext context)
    {
        _bags = context.Bags;
    }

    public async Task Add(Bag bag, CancellationToken cancellationToken)
        => await _bags.InsertOneAsync(bag, cancellationToken: cancellationToken);

    public async Task Update(Bag bag, CancellationToken cancellationToken)
    {
        var filter = Builders<Bag>.Filter.Eq(e => e.Id, bag.Id);
        await _bags.ReplaceOneAsync(filter, bag, cancellationToken: cancellationToken);
    }

    public async Task Remove(Bag bag, CancellationToken cancellationToken)
    {
        var filter = Builders<Bag>.Filter.Eq(e => e.Id, bag.Id);
        await _bags.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<Bag?> FindById(Guid requestId, CancellationToken cancellationToken)
    {
        var filter = Builders<Bag>.Filter.Eq(x => x.Id, requestId);
        return await _bags.Find(filter).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Bag?> FindBagByUserId(Guid requestUserId, CancellationToken cancellationToken)
    {
        var filter = Builders<Bag>.Filter.Eq(x => x.UserId, requestUserId);
        return await _bags.Find(filter).SingleOrDefaultAsync(cancellationToken);
    }
}