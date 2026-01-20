using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Repositories;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Users.Repositories;

public class CardRepository : Repository<Card>, ICardRepository
{
    public CardRepository(ApplicationDbContext context) : base(context)
    {
    }
}