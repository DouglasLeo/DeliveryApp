using DeliveryApp.Application.Shared.Abstractions.Repositories;
using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Domain.Entities.User;

namespace DeliveryApp.Application.Users.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> UserExists(Guid userId, CancellationToken cancellationToken);
    Task<UserDto?> FindUserDtoByEmail(string email, CancellationToken cancellationToken);
    Task<User?> FindUserByEmail(string email, CancellationToken cancellationToken);
    IQueryable<UserDto> FindAllUsersOrderedByName();
    Task<UserDto?> FindUserById(Guid requestId, CancellationToken cancellationToken);
    Task<User?> FindUserWithAddress(Guid id, CancellationToken cancellationToken);
    Task<User?> FindUserWithCards(Guid id, CancellationToken cancellationToken);
    Task<User?> ExistsUserWithCardId(Guid cardId, CancellationToken cancellationToken);
}