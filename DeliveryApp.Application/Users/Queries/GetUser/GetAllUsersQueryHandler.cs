using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Common.Models;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using MediatR;

namespace DeliveryApp.Application.Users.Queries.GetUser;

public record GetAllUserQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<UserDto>>;

public class GetAllUsersQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetAllUserQuery, PaginatedList<UserDto>>
{
    public async Task<PaginatedList<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        => await userRepository.GetAllUsersOrderedByName()
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
}