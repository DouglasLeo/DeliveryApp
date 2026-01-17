using DeliveryApp.Application.Bags.Abstractions.Repositories;
using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Bags.Queries.GetBag;

public record GetBagByUserIdQuery(Guid UserId) : IRequest<BagDto>;

public class GetBagByUserId(IBagRepository bagRepository, IFoodRepository foodRepository) : IRequestHandler<GetBagByUserIdQuery, BagDto>
{
    public async Task<BagDto> Handle(GetBagByUserIdQuery request, CancellationToken cancellationToken)
    {
        var bag = await bagRepository.FindBagByUserId(request.UserId, cancellationToken) ?? throw new NotFoundException($"Bag of user {request.UserId} not found");
        var foods = await foodRepository.GetFoodDtosByIds(bag.FoodsIds, cancellationToken);
        
        return bag.ToDto(foods);
    }
}