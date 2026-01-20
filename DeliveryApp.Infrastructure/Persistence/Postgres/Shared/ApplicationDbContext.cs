using System.Reflection;
using DeliveryApp.Application.Shared.Abstractions;
using DeliveryApp.Domain.Entities.Address;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Domain.Entities.Order;
using DeliveryApp.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Shared;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Card> CreditCards { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<FoodCategory> FoodCategories { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}