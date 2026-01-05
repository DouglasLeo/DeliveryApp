using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<Guid>;

public class DeleteUserHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, Guid>
{
    public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindById(request.Id, cancellationToken);
        if (user == null) throw new NotFoundException("Usuario não encontrado");

        user.Active = false;

        await userRepository.Update(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);

        return user.Id;
    }
}