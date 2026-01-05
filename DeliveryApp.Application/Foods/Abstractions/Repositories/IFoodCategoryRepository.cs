using DeliveryApp.Application.Shared.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;

namespace DeliveryApp.Application.Foods.Abstractions.Repositories;

public interface IFoodCategoryRepository : IRepository<FoodCategory>
{
    Task<FoodCategory> FindByName(string name, CancellationToken cancellationToken);
    IQueryable<FoodCategory> GetAllFoodCategories(int pageNumber, int pageSize, CancellationToken cancellationToken);
}