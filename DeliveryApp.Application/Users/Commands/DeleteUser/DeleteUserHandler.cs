using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest;

public class DeleteUserHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindById(request.Id, cancellationToken) ?? throw new NotFoundException("Usuario não encontrado");

        user.InactivateUser();

        await userRepository.Update(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);
    }
}