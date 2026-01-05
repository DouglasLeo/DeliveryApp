using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Domain.Entities.Order;
using DeliveryApp.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Shared.Abstractions;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<CreditCard> CreditCards { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<Address> Addresses { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderItems> OrderItems { get; set; }
    DbSet<Food> Foods { get; set; }
    DbSet<FoodCategory> FoodCategories { get; set; }
    DbSet<Tag> Tags { get; set; }
}