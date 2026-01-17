using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Infrastructure.Persistence.Shared;
using DeliveryApp.Infrastructure.Persistence.Shared.Repositories;

namespace DeliveryApp.Infrastructure.Persistence.Foods.Repositories;

public class FoodImageRepository : Repository<FoodImage>, IFoodImageRepository
{
    public FoodImageRepository(ApplicationDbContext context) : base(context)
    {
    }
}