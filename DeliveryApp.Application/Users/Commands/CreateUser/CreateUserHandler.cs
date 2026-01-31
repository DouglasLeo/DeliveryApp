using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Domain.Exceptions;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace DeliveryApp.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Name, string Email, string Password) : IRequest<Guid>;

public class CreateUserHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = await userRepository.FindUserByEmail(request.Email, cancellationToken);

        if (email is not null) throw new ConflictException("Email already exists");

        var passwordHash = BC.HashPassword(request.Password);

        var user = User.Create(request.Name, request.Email, passwordHash);

        await userRepository.Add(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);

        return user.Id;
    }
}