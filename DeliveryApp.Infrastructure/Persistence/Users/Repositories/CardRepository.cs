using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Infrastructure.Persistence.Shared;
using DeliveryApp.Infrastructure.Persistence.Shared.Repositories;

namespace DeliveryApp.Infrastructure.Persistence.Users.Repositories;

public class CardRepository : Repository<Card>, ICardRepository
{
    public CardRepository(ApplicationDbContext context) : base(context)
    {
    }
}