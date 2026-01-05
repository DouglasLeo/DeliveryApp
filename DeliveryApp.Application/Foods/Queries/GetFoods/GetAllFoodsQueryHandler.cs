using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Foods.Queries.DTOs;
using MediatR;

namespace DeliveryApp.Application.Foods.Queries.GetFoods;

public record GetAllFoodsQuery(int Skip = 0, int Take = 100) : IRequest<IEnumerable<FoodDto>>;

public class GetAllFoodsQueryHandler(IFoodRepository foodRepository)
    : IRequestHandler<GetAllFoodsQuery, IEnumerable<FoodDto>>
{
    public async Task<IEnumerable<FoodDto>> Handle(GetAllFoodsQuery request, CancellationToken cancellationToken)
        => await foodRepository.GetAllFoods(request.Skip, request.Take, cancellationToken);
}