using DeliveryApp.Application.Bags.Queries;
using DeliveryApp.Domain.Entities.Bag;

namespace DeliveryApp.Application.Bags.Abstractions.Repositories;

public interface IBagRepository
{
    Task Add(Bag bag, CancellationToken cancellationToken);
    Task Update(Bag bag, CancellationToken cancellationToken);
    Task Remove(Bag bag, CancellationToken cancellationToken);
    Task<Bag?> FindById(Guid id, CancellationToken cancellationToken);
    Task<Bag?> FindBagByUserId(Guid userId, CancellationToken cancellationToken);
}