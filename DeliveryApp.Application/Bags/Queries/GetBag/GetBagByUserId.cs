using DeliveryApp.Application.Bags.Abstractions.Repositories;
using MediatR;

namespace DeliveryApp.Application.Bags.Queries.GetBag;

public record GetBagByUserIdQuery(Guid Id) : IRequest<BagDto>;

public class GetBagByUserId(IBagRepository bagRepository) : IRequestHandler<GetBagByUserIdQuery, BagDto>
{
    public async Task<BagDto> Handle(GetBagByUserIdQuery request, CancellationToken cancellationToken)
        => await bagRepository.FindBagByUserId(request.Id, cancellationToken);
}