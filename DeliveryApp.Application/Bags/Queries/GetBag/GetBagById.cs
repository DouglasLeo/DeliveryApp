using DeliveryApp.Application.Bags.Abstractions.Repositories;
using DeliveryApp.Application.Bags.Queries.Dtos;
using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Bags.Queries.GetBag;

public record GetBagByIdQuery(Guid Id) : IRequest<BagDto>;

public class GetBagById(IBagRepository bagRepository, IFoodRepository foodRepository)
    : IRequestHandler<GetBagByIdQuery, BagDto>
{
    public async Task<BagDto> Handle(GetBagByIdQuery request, CancellationToken cancellationToken)
    {
        var bag = await bagRepository.FindById(request.Id, cancellationToken) ??
                  throw new NotFoundException($"Bag of id {request.Id} not found");
        var foods = await foodRepository.GetFoodDtosByIds(bag.BagItems.Select(b => b.FoodId), cancellationToken);

        return bag.ToDto(foods);
    }
}