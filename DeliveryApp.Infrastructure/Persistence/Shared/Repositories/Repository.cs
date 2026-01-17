using System.Linq.Expressions;
using DeliveryApp.Application.Shared.Abstractions.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Persistence.Shared.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> DbSet;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }

    public async Task<List<T>> FindAll(int skip, int take, CancellationToken cancellationToken) =>
        await DbSet.AsNoTracking().Skip(skip).Take(take).ToListAsync(cancellationToken);

    public async Task<T?> FindById(Guid id, CancellationToken cancellationToken) =>
        await DbSet.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) =>
        await DbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);

    public async Task Add(T entity, CancellationToken cancellationToken) =>
        await DbSet.AddAsync(entity, cancellationToken);

    public async Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken) =>
        await DbSet.AddRangeAsync(entities, cancellationToken);

    public async Task Update(T entity, CancellationToken cancellationToken) =>
        await Task.Run(() => DbSet.Update(entity), cancellationToken);

    public async Task Remove(T entity, CancellationToken cancellationToken) =>
        await Task.Run(() => DbSet.Remove(entity), cancellationToken);

    public async Task<int> SaveChanges(CancellationToken cancellationToken) =>
        await Context.SaveChangesAsync(cancellationToken);
}