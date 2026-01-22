using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Users.Repositories;

public class CardRepository : Repository<Card>, ICardRepository
{
    public CardRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CardDto>> FindAllCardsByUserId(Guid requestUserId,
        CancellationToken cancellationToken) =>
        await DbSet.AsNoTracking().Where(c => c.UserId == requestUserId).ProjectToModel()
            .ToListAsync(cancellationToken);

    public async Task<CardDto?> FindCardById(Guid requestId, CancellationToken cancellationToken) => await DbSet
        .AsNoTracking().Where(c => c.Id == requestId).ProjectToModel().SingleOrDefaultAsync(cancellationToken);
}