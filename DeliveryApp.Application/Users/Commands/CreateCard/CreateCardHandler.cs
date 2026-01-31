using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Commands.CreateCard;

public record CreateCardCommand(
    Guid UserId,
    string Token,
    string CardFinalNumbers,
    ECardType Type,
    ECardBrand Brand) : IRequest<Guid>;

public class CreateCardHandler(ICardRepository cardRepository, IUserRepository userRepository)
    : IRequestHandler<CreateCardCommand, Guid>
{
    private const int CardsLimit = 10;

    public async Task<Guid> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindUserWithCards(request.UserId, cancellationToken) ??
                   throw new NotFoundException("User not found");

        if (user.Cards.Count >= CardsLimit) throw new LimitExceededException("Cards", CardsLimit);

        var card = Card.Create(request.UserId, request.Token,
            request.Type, request.Brand, request.CardFinalNumbers);

        user.UpdateCard(card.Id);

        await userRepository.Update(user, cancellationToken);
        await cardRepository.Add(card, cancellationToken);
        await cardRepository.SaveChanges(cancellationToken);

        return card.Id;
    }
}