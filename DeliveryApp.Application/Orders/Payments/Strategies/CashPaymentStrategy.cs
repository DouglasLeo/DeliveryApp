using DeliveryApp.Application.Orders.Payments.Abstractions;
using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.Orders.Payments.Strategies;

public class CashPaymentStrategy : IPaymentStrategy
{
    public EPaymentMethod Method => EPaymentMethod.Cash;

    public Task<Card?> ResolveAsync(
        User _,
        CancellationToken __)
        => Task.FromResult<Card?>(null);
}