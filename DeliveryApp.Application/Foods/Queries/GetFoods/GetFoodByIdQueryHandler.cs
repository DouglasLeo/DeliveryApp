using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Foods.Queries.GetFoods;

public record GetFoodByIdQuery(Guid FoodId) : IRequest<FoodDto>;

public class GetFoodByIdQueryHandler(IFoodRepository foodRepository) : IRequestHandler<GetFoodByIdQuery, FoodDto>
{
    public async Task<FoodDto> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
        => await foodRepository.GetFoodById(request.FoodId, cancellationToken) ?? throw new NotFoundException("Food not found");
}