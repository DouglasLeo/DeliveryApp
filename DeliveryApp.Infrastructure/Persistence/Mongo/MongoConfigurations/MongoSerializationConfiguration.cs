using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace DeliveryApp.Infrastructure.Persistence.Mongo.MongoConfigurations;

public class MongoSerializationConfiguration
{
    private static bool _initialized;

    public static void Configure()
    {
        if (_initialized) return;

        BsonSerializer.RegisterSerializer(
            new GuidSerializer(GuidRepresentation.Standard)
        );

        _initialized = true;
    }
}