using DeliveryApp.Application.Shared.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;

namespace DeliveryApp.Application.Foods.Abstractions.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> FindByName(string name, CancellationToken cancellationToken);
    IQueryable<Tag> GetAllTags();
    Task<IEnumerable<Tag>> FindTagsByIds(IEnumerable<Guid> tagId, CancellationToken cancellationToken);
}