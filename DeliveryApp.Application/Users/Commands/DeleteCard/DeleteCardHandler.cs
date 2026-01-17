using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Commands.DeleteCard;

public record DeleteCardCommand(Guid CardId) : IRequest;

public class DeleteCardHandler(ICardRepository cardRepository, IUserRepository userRepository)
    : IRequestHandler<DeleteCardCommand>
{
    public async Task Handle(DeleteCardCommand request, CancellationToken cancellationToken)
    {
        var card = await cardRepository.FindById(request.CardId, cancellationToken) ?? throw new NotFoundException("Card not found.");
        var userUsingCard = await userRepository.ExistsUserWithCardId(request.CardId, cancellationToken);

        if (userUsingCard != null)
        {
            userUsingCard.RemoveCard();;
            await userRepository.Update(userUsingCard, cancellationToken);
        }
        
        await cardRepository.Remove(card, cancellationToken);
        await cardRepository.SaveChanges(cancellationToken);
    }
}