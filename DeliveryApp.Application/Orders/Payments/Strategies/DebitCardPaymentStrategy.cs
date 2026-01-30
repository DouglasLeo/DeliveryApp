using DeliveryApp.Application.Orders.Payments.Abstractions;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Exceptions;

namespace DeliveryApp.Application.Orders.Payments.Strategies;

public class DebitCardPaymentStrategy(ICardRepository cardRepository) : IPaymentStrategy
{
    public EPaymentMethod Method => EPaymentMethod.DebitCard;

    public async Task<Card?> ResolveAsync(
        User user,
        CancellationToken cancellationToken)
    {
        if (user.CardId is null)
            throw new NotDefinedException("Card");

        return await cardRepository.FindById(
                   user.CardId.Value,
                   cancellationToken)
               ?? throw new NotFoundException("User card not found");
    }
}