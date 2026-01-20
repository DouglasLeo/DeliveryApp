using DeliveryApp.Domain;
using DeliveryApp.Domain.Entities.Bag;
using MongoDB.Driver;

namespace DeliveryApp.Infrastructure.Persistence.Mongo;

public sealed class MongoDbContext
{
    public readonly IMongoDatabase Database;

    public MongoDbContext()
    {
        var client = new MongoClient(Configuration.NoSqlDatabase.ConnectionString);
        Database = client.GetDatabase(Configuration.NoSqlDatabase.DatabaseName);
    }

    public IMongoCollection<Bag> Bags => Database.GetCollection<Bag>("Bags");
}