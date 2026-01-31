using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Queries.GetCard;

public record GetCardByIdQuery(Guid Id) : IRequest<CardDto>;

public class GetCardByIdHandler(ICardRepository cardRepository) : IRequestHandler<GetCardByIdQuery, CardDto>
{
    public async Task<CardDto> Handle(GetCardByIdQuery request, CancellationToken cancellationToken) =>
        await cardRepository.FindCardById(request.Id, cancellationToken) ?? throw new NotFoundException("Card not found");
}