using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Commands.UpdateUserCard;

public record UpdateUserCardCommand(Guid UserId, Guid CardId) : IRequest;

public class UpdaterUserCardHandler(IUserRepository userRepository, ICardRepository cardRepository) : IRequestHandler<UpdateUserCardCommand>
{
    public async Task Handle(UpdateUserCardCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindById(request.UserId, cancellationToken) ?? throw new NotFoundException("User not found");
        var card = await cardRepository.FindById(request.CardId, cancellationToken) ?? throw new NotFoundException("Card not found");

        if (card.UserId != user.Id) throw new MismatchException("card", "user");
        
        user.UpdateCard(card.Id);
        
        await userRepository.Update(user, cancellationToken);
        await userRepository.SaveChanges(cancellationToken);
    }
}