using DeliveryApp.Application.Bags.Queries;
using DeliveryApp.Domain.Entities.Bag;

namespace DeliveryApp.Application.Bags.Abstractions.Repositories;

public interface IBagRepository
{
    Task Add(Bag bag, CancellationToken cancellationToken);
    Task Update(Bag bag, CancellationToken cancellationToken);
    Task Remove(Bag bag, CancellationToken cancellationToken);
    Task<int> SaveChanges(CancellationToken cancellationToken);
    Task<Bag> FindById(Guid requestId, CancellationToken cancellationToken);
    Task<BagDto> FindBagByUserId(Guid requestUserId, CancellationToken cancellationToken);
    Task<BagDto> FindBagById(Guid requestId, CancellationToken cancellationToken);
}