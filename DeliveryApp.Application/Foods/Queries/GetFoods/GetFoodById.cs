using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Foods.Queries.DTOs;
using MediatR;

namespace DeliveryApp.Application.Foods.Queries.GetFoods;

public record GetFoodByIdQuery(Guid FoodId) : IRequest<FoodDto>;

public class GetFoodById(IFoodRepository foodRepository) : IRequestHandler<GetFoodByIdQuery, FoodDto>
{
    public async Task<FoodDto> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
        => await foodRepository.GetFoodById(request.FoodId, cancellationToken);
}