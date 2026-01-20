namespace DeliveryApp.Domain;

public class Configuration
{
    public static SecretsConfiguration Secrets { get; set; } = new();
    public static DatabaseConfiguration SqlDatabase { get; set; } = new();
    public static DatabaseConfiguration NoSqlDatabase { get; set; } = new();

    public class SecretsConfiguration
    {
        public string JwtPrivateKey { get; set; } = string.Empty;
    }

    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; }
    }
}