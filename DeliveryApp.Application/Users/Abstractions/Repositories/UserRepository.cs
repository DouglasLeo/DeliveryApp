using DeliveryApp.Application.Shared.Abstractions.Repositories;
using DeliveryApp.Application.Users.Queries;
using DeliveryApp.Domain.Entities.User;

namespace DeliveryApp.Application.Users.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> UserExists(Guid userId, CancellationToken cancellationToken);
    Task<UserDto> FindByEmail(string email, CancellationToken cancellationToken);
    IQueryable<UserDto> GetAllUsersOrderedByName(CancellationToken cancellationToken);
    Task<UserDto> FindUserById(Guid requestId, CancellationToken cancellationToken);
}