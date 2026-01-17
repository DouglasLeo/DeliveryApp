using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Orders.Abstractions.Repositories;
using DeliveryApp.Application.Orders.Queries;
using DeliveryApp.Domain.Entities.Order;
using DeliveryApp.Infrastructure.Persistence.Shared;
using DeliveryApp.Infrastructure.Persistence.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Persistence.Orders.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderDto>> FindAllOrdersById(Guid userId, CancellationToken cancellationToken) =>
        await DbSet
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToDto()
            .ToListAsync(cancellationToken);

    public async Task<OrderDto?> FindOrderById(Guid id, CancellationToken cancellationToken) =>
        (await DbSet
            .AsNoTracking()
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken))?.ToDto();
}