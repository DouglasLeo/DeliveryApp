using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Foods.Queries.GetFoods;

public record GetFoodByIdWithInactivesQuery(Guid FoodId) : IRequest<FoodDto>;

public class GetFoodByIdWithInactivesQueryHandler(IFoodRepository foodRepository) : IRequestHandler<GetFoodByIdQuery, FoodDto>
{
    public async Task<FoodDto> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
        => await foodRepository.GetFoodByIdWithInactives(request.FoodId, cancellationToken) ?? throw new NotFoundException("Food not found");
}