using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Infrastructure.Persistence.Shared;
using DeliveryApp.Infrastructure.Persistence.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Persistence.Foods.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Tag?> FindByName(string name, CancellationToken cancellationToken)
        => await DbSet.AsNoTracking().SingleOrDefaultAsync(t => t.Name == name, cancellationToken);

    public IQueryable<Tag> GetAllTags()
        => DbSet.OrderBy(t => t.Name);

    public async Task<IEnumerable<Tag>> FindTagsByIds(IEnumerable<Guid> tagId, CancellationToken cancellationToken)
        => await DbSet.AsNoTracking().Where(t => tagId.Contains(t.Id)).ToListAsync(cancellationToken);
}