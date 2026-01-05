using DeliveryApp.Application.Shared.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Order;

namespace DeliveryApp.Application.Orders.Abstractions.Repositories;

public interface IOrderItemsRepository : IRepository<OrderItems>
{
}