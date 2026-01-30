using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Foods.Repositories;

public class FoodRepository : Repository<Food>, IFoodRepository
{
    public FoodRepository(ApplicationDbContext context) : base(context)
    {
    }

    private IQueryable<Food> FoodDtoBaseQuery() => DbSet.AsNoTracking().Include(f => f.FoodImage)
        .Include(f => f.FoodCategory).Include(f => f.Tags).Where(f => f.Active);

    private IQueryable<Food> FoodDtoBaseQueryWithInactives() =>
        DbSet.AsNoTracking().Include(f => f.FoodImage).Include(f => f.FoodCategory).Include(f => f.Tags);

    public async Task<IEnumerable<FoodDto>> GetAllFoods(int skip, int take, CancellationToken cancellationToken) =>
        await FoodDtoBaseQuery().Skip(skip).Take(take).ProjectToModel().ToListAsync(cancellationToken);

    public async Task<FoodDto?> GetFoodById(Guid id, CancellationToken cancellationToken) =>
        (await FoodDtoBaseQuery().SingleOrDefaultAsync(f => f.Id == id, cancellationToken))?.ToDto();

    public async Task<IEnumerable<FoodDto>> GetAllFoodsWithInactives(int skip, int take,
        CancellationToken cancellationToken) =>
        await FoodDtoBaseQueryWithInactives().Skip(skip).Take(take).ProjectToModel().ToListAsync(cancellationToken);

    public async Task<FoodDto?> GetFoodByIdWithInactives(Guid id, CancellationToken cancellationToken) =>
        (await FoodDtoBaseQuery().SingleOrDefaultAsync(f => f.Id == id, cancellationToken))?.ToDto();

    public async Task<IEnumerable<FoodDto>> GetFoodDtosByIds(IEnumerable<Guid> requestItemsIds,
        CancellationToken cancellationToken) =>
        await DbSet.AsNoTracking().Include(f => f.FoodImage).Where(f => requestItemsIds.Any(id => id == f.Id))
            .ProjectToModel()
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Food>> GetFoodsByIds(IEnumerable<Guid> requestItemsIds,
        CancellationToken cancellationToken) =>
        await DbSet.AsNoTracking().Where(f => requestItemsIds.Any(id => id == f.Id))
            .ToListAsync(cancellationToken);
}