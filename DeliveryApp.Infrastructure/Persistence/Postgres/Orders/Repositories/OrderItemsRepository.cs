using DeliveryApp.Application.Orders.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Order;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Repositories;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Orders.Repositories;

public class OrderItemsRepository : Repository<OrderItems>, IOrderItemsRepository
{
    public OrderItemsRepository(ApplicationDbContext context) : base(context)
    {
    }
}