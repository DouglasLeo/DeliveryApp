using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Orders.Abstractions.Repositories;
using DeliveryApp.Application.Orders.Queries;
using DeliveryApp.Domain.Entities.Order;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Orders.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    private IQueryable<Order> BaseQuery() => DbSet
        .AsNoTracking()
        .Include(x => x.OrderItems)
        .ThenInclude(oi => oi.Food)
        .ThenInclude(f => f.FoodCategory)
        .Include(x => x.OrderItems)
        .ThenInclude(oi => oi.Food)
        .ThenInclude(f => f.Tags);

    public async Task<IEnumerable<OrderDto>> FindAllOrdersById(Guid userId, CancellationToken cancellationToken) =>
        await BaseQuery()
            .Where(x => x.UserId == userId)
            .ToDto()
            .ToListAsync(cancellationToken);

    public async Task<OrderDto?> FindOrderById(Guid id, CancellationToken cancellationToken) =>
        (await BaseQuery()
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken))?.ToDto();
}