using DeliveryApp.Domain;

namespace DeliveryApp.API.Extensions;

public static class BuilderExtensions
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.SqlDatabase.ConnectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        Configuration.NoSqlDatabase.ConnectionString = 
            builder.Configuration.GetConnectionString("MongoConnection") ?? string.Empty;
        Configuration.NoSqlDatabase.DatabaseName = "deliveryAppDB";
    }
}