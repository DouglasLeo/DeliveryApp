using DeliveryApp.Application.Adresses.Abstractions;
using MediatR;

namespace DeliveryApp.Application.Adresses.Queries.GetAddress;

public record GetAddressesByUserIdQuery(Guid UserId) : IRequest<IEnumerable<AddressDto>>;

public class GetAllAddressesByUserId(IAddressRepository addressRepository)
    : IRequestHandler<GetAddressesByUserIdQuery, IEnumerable<AddressDto>>
{
    public async Task<IEnumerable<AddressDto>> Handle(GetAddressesByUserIdQuery request,
        CancellationToken cancellationToken) =>
        await addressRepository.FindAllByUserId(request.UserId, cancellationToken);
}