using DeliveryApp.Application.Adresses.Abstractions;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Adresses.Commands.UpdateAddress;

public record UpdateAddressCommand(
    Guid Id,
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Neighboorhood,
    string Country,
    string? Complement,
    string? Reference) : IRequest;

public class UpdateAddressHandler(IAddressRepository addressRepository) : IRequestHandler<UpdateAddressCommand>
{
    public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await addressRepository.FindById(request.Id, cancellationToken) ??
                      throw new NotFoundException("Address not found");

        address.Update(request.Street, request.HouseNumber, request.PostalCode, request.City, request.Neighboorhood,
            request.Country, request.Complement, request.Reference);

        await addressRepository.Update(address, cancellationToken);
        await addressRepository.SaveChanges(cancellationToken);
    }
}