using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Queries.GetUser;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        await userRepository.FindUserById(request.Id, cancellationToken) ??
        throw new NotFoundException("User not found");
}