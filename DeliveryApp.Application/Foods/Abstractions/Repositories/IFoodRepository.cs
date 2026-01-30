using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Application.Shared.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;

namespace DeliveryApp.Application.Foods.Abstractions.Repositories;

public interface IFoodRepository : IRepository<Food>
{
    Task<IEnumerable<FoodDto>> GetAllFoods(int skip, int take, CancellationToken cancellationToken);
    Task<FoodDto?> GetFoodById(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<FoodDto>> GetFoodDtosByIds(IEnumerable<Guid> requestItemsIds, CancellationToken cancellationToken);
    Task<IEnumerable<FoodDto>> GetAllFoodsWithInactives(int skip, int take, CancellationToken cancellationToken);
    Task<FoodDto?> GetFoodByIdWithInactives(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Food>> GetFoodsByIds(IEnumerable<Guid> requestItemsIds, CancellationToken cancellationToken);
}