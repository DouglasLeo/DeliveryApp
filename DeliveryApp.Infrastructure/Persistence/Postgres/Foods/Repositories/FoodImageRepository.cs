using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Repositories;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Foods.Repositories;

public class FoodImageRepository : Repository<FoodImage>, IFoodImageRepository
{
    public FoodImageRepository(ApplicationDbContext context) : base(context)
    {
    }
}