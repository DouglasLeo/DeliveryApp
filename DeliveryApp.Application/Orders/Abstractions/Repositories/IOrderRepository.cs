using DeliveryApp.Application.Orders.Queries;
using DeliveryApp.Application.Shared.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Order;

namespace DeliveryApp.Application.Orders.Abstractions.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<OrderDto>> FindAllOrdersById(Guid requestUserId, CancellationToken cancellationToken);
    Task<OrderDto> FindOrderById(Guid requestUserId, CancellationToken cancellationToken);
}