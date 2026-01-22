using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid Id, string Name, string Email) : IRequest;

public class UpdateUserHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindById(request.Id, cancellationToken);

        if (user is null) throw new NotFoundException("User not found");

        user.Update(request.Name, request.Email);

        await userRepository.Update(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);
    }
}