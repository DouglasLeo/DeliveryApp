using DeliveryApp.Application.Adresses.Abstractions;
using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Adresses.Queries.GetAddress;

public record GetAddressByIdQuery(Guid Id) : IRequest<AddressDto>;

public class GetAddressById(IAddressRepository addressRepository) : IRequestHandler<GetAddressByIdQuery, AddressDto>
{
    public async Task<AddressDto> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken) =>
        AddressDtoMapperly.FromEntity(await addressRepository.FindById(request.Id, cancellationToken) ??
                                    throw new NotFoundException("Address not found"));
}