using System.Linq.Expressions;

namespace DeliveryApp.Application.Shared.Abstractions.Repositories;

public interface IRepository<TEntity>
{
    Task<List<TEntity>> FindAll(int skip, int take, CancellationToken cancellationToken);
    Task<TEntity?> FindById(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task Add(TEntity entity, CancellationToken cancellationToken);
    Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task Update(TEntity entity, CancellationToken cancellationToken);
    Task Remove(TEntity entity, CancellationToken cancellationToken);
    Task<int> SaveChanges(CancellationToken cancellationToken);
}