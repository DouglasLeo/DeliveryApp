using DeliveryApp.Application.Adresses.Abstractions;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Address;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Adresses.Commands.CreateAddress;

public record CreateAddressCommand(
    Guid UserId,
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Neighboorhood,
    string Country,
    string Complement,
    string? Reference) : IRequest<Guid>;

public class CreateAddressHandler(IAddressRepository addressRepository, IUserRepository userRepository)
    : IRequestHandler<CreateAddressCommand, Guid>
{
    private const int Addresseslimit = 10;

    public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindUserWithAddress(request.UserId, cancellationToken) ??
                   throw new NotFoundException("User not found");

        if (user.Addresses.Count > Addresseslimit) throw new LimitExceededException("Addresses", Addresseslimit);

        var address = Address.Create(request.UserId, request.Street, request.HouseNumber, request.PostalCode,
            request.City, request.Neighboorhood, request.Country, request.Complement,request.Reference);

        user.UpdateMainAddress(address);

        await addressRepository.Add(address, cancellationToken);
        await userRepository.Update(user, cancellationToken);
        await addressRepository.SaveChanges(cancellationToken);

        return address.Id;
    }
}