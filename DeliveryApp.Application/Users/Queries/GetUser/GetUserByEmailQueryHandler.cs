using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Queries.GetUser;

public record GetUserByEmailQuery(string Email) : IRequest<UserDto>;

public class GetUserByEmailQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByEmailQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken) =>
        await userRepository.FindUserDtoByEmail(request.Email, cancellationToken) ??
        throw new NotFoundException("User not found");
}