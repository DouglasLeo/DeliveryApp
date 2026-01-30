using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.Orders.Payments.Abstractions;

public interface IPaymentStrategy
{
    EPaymentMethod Method { get; }

    Task<Card?> ResolveAsync(User user, CancellationToken cancellationToken);
}