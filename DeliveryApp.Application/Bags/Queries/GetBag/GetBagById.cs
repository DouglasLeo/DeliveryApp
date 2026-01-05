using DeliveryApp.Application.Bags.Abstractions.Repositories;
using MediatR;

namespace DeliveryApp.Application.Bags.Queries.GetBag;

public record GetBagByIdQuery(Guid Id) : IRequest<BagDto>;

public class GetBagById(IBagRepository bagRepository) : IRequestHandler<GetBagByIdQuery, BagDto>
{
    public async Task<BagDto> Handle(GetBagByIdQuery request, CancellationToken cancellationToken)
        => await bagRepository.FindBagById(request.Id, cancellationToken);
}