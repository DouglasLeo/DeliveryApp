using DeliveryApp.Application.Orders.Payments.Abstractions;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.Orders.Payments;

public class PaymentStrategyFactory
{
    private readonly IReadOnlyDictionary<EPaymentMethod, IPaymentStrategy> _strategies;

    public PaymentStrategyFactory(IEnumerable<IPaymentStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(s => s.Method);
    }

    public IPaymentStrategy Get(EPaymentMethod method) =>
        _strategies.TryGetValue(method, out var strategy)
            ? strategy
            : throw new NotSupportedException(
                $"Payment method '{method}' is not supported");
}