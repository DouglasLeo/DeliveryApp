using DeliveryApp.Domain.Entities.Bag;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DeliveryApp.Infrastructure.Persistence.Shared;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration config)
    {
        var connectionString = config["MongoDB:ConnectionString"];
        var databaseName = config["MongoDB:Database"];

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Bag> Bags => _database.GetCollection<Bag>("Bags");
}