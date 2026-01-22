using DeliveryApp.Application.Shared.Abstractions.Repositories;
using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Domain.Entities.User;

namespace DeliveryApp.Application.Users.Abstractions.Repositories;

public interface ICardRepository : IRepository<Card>
{
    Task<IEnumerable<CardDto>> FindAllCardsByUserId(Guid requestUserId, CancellationToken cancellationToken);
    Task<CardDto?> FindCardById(Guid requestId, CancellationToken cancellationToken);
}