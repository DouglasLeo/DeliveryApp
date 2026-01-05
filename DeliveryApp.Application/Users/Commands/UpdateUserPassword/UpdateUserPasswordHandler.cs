using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace DeliveryApp.Application.Users.Commands.UpdateUserPassword;

public record UpdateUserPasswordCommand(Guid Id, string Password) : IRequest<Guid>;

public class UpdateUserPasswordHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateUserPasswordCommand, Guid>
{
    public async Task<Guid> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindById(request.Id, cancellationToken);

        if (user is null) throw new NotFoundException("User not found");

        user.Password = BC.HashPassword(request.Password);

        await userRepository.Update(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);

        return user.Id;
    }
}