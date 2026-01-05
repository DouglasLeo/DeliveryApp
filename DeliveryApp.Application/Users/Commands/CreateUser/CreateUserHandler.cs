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
        var email = await userRepository.FindByEmail(request.Email, cancellationToken);

        if (email is not null) throw new AlreadyExistsException("Email already exists");

        var passwordHash = BC.HashPassword(request.Password);

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = passwordHash
        };

        await userRepository.Add(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);

        return user.Id;
    }
}