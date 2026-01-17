using DeliveryApp.Application.Adresses.Abstractions;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Adresses.Commands.DeleteAddress;

public record DeleteAddressCommand(
    Guid Id) : IRequest<Guid>;

public class DeleteAddressHandler(IAddressRepository addressRepository) : IRequestHandler<DeleteAddressCommand, Guid>
{
    public async Task<Guid> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await addressRepository.FindById(request.Id, cancellationToken) ??
                      throw new NotFoundException("Address not found");
        
        await addressRepository.Remove(address, cancellationToken);
        await addressRepository.SaveChanges(cancellationToken);

        return address.Id;
    }
}