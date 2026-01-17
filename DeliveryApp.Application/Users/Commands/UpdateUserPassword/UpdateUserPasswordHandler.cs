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
        var user = await userRepository.FindById(request.Id, cancellationToken) ?? throw new NotFoundException("User not found");

        user.UpdatePassword(BC.HashPassword(request.Password));
//TODO: permitir apenas o token que bata com o id do request, ou seja só o dono da conta pode alterar a propria senha
        await userRepository.Update(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);

        return user.Id;
    }
}