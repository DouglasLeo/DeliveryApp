using DeliveryApp.Application.Adresses.Abstractions;
using DeliveryApp.Application.Bags.Abstractions.Repositories;
using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Orders.Abstractions.Repositories;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain;
using DeliveryApp.Infrastructure.Persistence.Mongo;
using DeliveryApp.Infrastructure.Persistence.Mongo.Abstractions;
using DeliveryApp.Infrastructure.Persistence.Mongo.Bags;
using DeliveryApp.Infrastructure.Persistence.Mongo.Bags.Repositories;
using DeliveryApp.Infrastructure.Persistence.Mongo.MongoConfigurations;
using DeliveryApp.Infrastructure.Persistence.Postgres.Adresses.Repositories;
using DeliveryApp.Infrastructure.Persistence.Postgres.Foods.Repositories;
using DeliveryApp.Infrastructure.Persistence.Postgres.Orders.Repositories;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared;
using DeliveryApp.Infrastructure.Persistence.Postgres.Users.Repositories;
using DeliveryApp.Infrastructure.Services;
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
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(Configuration.SqlDatabase.ConnectionString, providerOptions =>
            {
                providerOptions.MigrationsHistoryTable("__ef_migrations_history");
                providerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
        services.AddScoped<IBagRepository, BagRepository>();
        services.AddScoped<IFoodRepository, FoodRepository>();
        services.AddScoped<IFoodCategoryRepository, FoodCategoryRepository>();
        services.AddScoped<IFoodImageRepository, FoodImageRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ICardRepository, CardRepository>();

        services.AddSingleton<IFileStorageService, FileStorageService>();
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