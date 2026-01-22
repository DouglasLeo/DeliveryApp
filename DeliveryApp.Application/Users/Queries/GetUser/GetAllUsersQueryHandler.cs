using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Common.Models;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Application.Users.Queries.Dtos;
using MediatR;

namespace DeliveryApp.Application.Users.Queries.GetUser;

public record GetAllUsersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<UserDto>>;

public class GetAllUsersQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetAllUsersQuery, PaginatedList<UserDto>>
{
    public async Task<PaginatedList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        => await userRepository.FindAllUsersOrderedByName()
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
}