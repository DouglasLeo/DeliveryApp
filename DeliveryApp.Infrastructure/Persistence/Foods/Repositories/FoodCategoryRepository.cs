using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Infrastructure.Persistence.Shared;
using DeliveryApp.Infrastructure.Persistence.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Persistence.Foods.Repositories;

public class FoodCategoryRepository : Repository<FoodCategory>, IFoodCategoryRepository
{
    public FoodCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<FoodCategory?> FindByName(string name, CancellationToken cancellationToken)
        => await DbSet.AsNoTracking().SingleOrDefaultAsync(t => t.Name == name, cancellationToken);

    public IQueryable<FoodCategory> GetAllFoodCategories() => DbSet.OrderBy(fc => fc.Name);
}