using DeliveryApp.Application.Adresses.Abstractions;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Commands.UpdateUserAddress;

public record UpdateUserMainAddressCommand(Guid UserId, Guid MainAddressId) : IRequest;

public class UpdateUserMainAddress(IUserRepository userRepository, IAddressRepository addressRepository) : IRequestHandler<UpdateUserMainAddressCommand>
{
    public async Task Handle(UpdateUserMainAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindById(request.UserId, cancellationToken) ?? throw new NotFoundException("User not found");
        var address = await addressRepository.FindById(request.MainAddressId, cancellationToken) ?? throw new NotFoundException("Address not found");

        if (address.UserId != user.Id) throw new MismatchException("address", "user");
        
        user.UpdateMainAddress(address);
        
        await userRepository.Update(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);
    }
}