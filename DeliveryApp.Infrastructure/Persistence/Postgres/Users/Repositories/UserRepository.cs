using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Users.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> UserExists(Guid userId, CancellationToken cancellationToken) =>
        await DbSet.AsNoTracking().AnyAsync(u => u.Id == userId, cancellationToken);

    public async Task<UserDto?> FindUserDtoByEmail(string email, CancellationToken cancellationToken) =>
        await DbSet
            .AsNoTracking()
            .Where(u => u.Email == email)
            .ProjectToModel()
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<User?> FindUserByEmail(string email, CancellationToken cancellationToken) =>
        await DbSet.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email, cancellationToken);

    public IQueryable<UserDto> FindAllUsersOrderedByName() =>
        DbSet.OrderBy(u => u.Name).ProjectToModel();

    public async Task<UserDto?> FindUserById(Guid requestId, CancellationToken cancellationToken) =>
        await DbSet
            .AsNoTracking()
            .Where(u => u.Id == requestId)
            .ProjectToModel()
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<User?> FindUserWithAddress(Guid id, CancellationToken cancellationToken) =>
        await DbSet.Include(u => u.Addresses).SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<User?> FindUserWithCards(Guid id, CancellationToken cancellationToken) =>
        DbSet.Include(u => u.Cards).SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<User?> ExistsUserWithCardId(Guid cardId, CancellationToken cancellationToken) =>
        DbSet.SingleOrDefaultAsync(u => u.CardId == cardId, cancellationToken);
}