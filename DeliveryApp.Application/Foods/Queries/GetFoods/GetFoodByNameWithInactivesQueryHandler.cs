using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Foods.Queries.DTOs;
using MediatR;

namespace DeliveryApp.Application.Foods.Queries.GetFoods;

public record GetFoodByNameWithInactivesQuery(string Name) : IRequest<IEnumerable<FoodDto>>;

public class GetFoodByNameWithInactivesQueryHandler(IFoodRepository foodRepository) : IRequestHandler<GetFoodByNameQuery, IEnumerable<FoodDto>>
{
    public async Task<IEnumerable<FoodDto>> Handle(GetFoodByNameQuery request, CancellationToken cancellationToken)
        => await foodRepository.GetFoodByNameWithInactives(request.Name, cancellationToken);
}