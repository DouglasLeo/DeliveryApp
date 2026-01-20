using DeliveryApp.Application.Shared.Abstractions;
using DeliveryApp.Domain;
using DeliveryApp.Infrastructure.Persistence.Mongo;
using DeliveryApp.Infrastructure.Persistence.Mongo.Abstractions;
using DeliveryApp.Infrastructure.Persistence.Mongo.Bags;
using DeliveryApp.Infrastructure.Persistence.Mongo.MongoConfigurations;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryApp.Infrastructure.DependencyInjection;

public static class InfrasctrutureDependency
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres();
        services.AddMongo();
    }

    private static void AddPostgres(
        this IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseNpgsql(Configuration.SqlDatabase.ConnectionString, providerOptions =>
            {
                providerOptions.MigrationsHistoryTable("__ef_migrations_history");
                providerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });
        });
    }

    private static void AddMongo(
        this IServiceCollection services)
    {
        services.AddSingleton<IMongoCollectionInitializer, BagCollectionInitializer>();
        
        services.AddSingleton<MongoDbContext>();

        services.AddSingleton<MongoInitializer>();

        services.AddHostedService<MongoInitializerHostedService>();
    }
}