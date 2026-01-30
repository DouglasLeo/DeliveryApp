using DeliveryApp.Domain.Entities.Shared;
using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Domain.Entities.Order;

public class Order : Entity
{
    public Guid UserId { get; set; }
    public User.User User { get; set; } = null!;
    public EOrderStatus OrderStatus { get; set; }
    public List<OrderItems> OrderItems { get; set; } = [];

    public EPaymentMethod PaymentMethod { get; set; }
    public string? CardFinalNumber { get; set; }

    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? Complement { get; set; }
    public string? Reference { get; set; }
    public string Neighboorhood { get; set; } = null!;

    public void Update(EOrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public static Order Create(Guid requestUserId, EOrderStatus requestOrderStatus, EPaymentMethod paymentMethod,
        Card? card, Address.Address address)
    {
        if (paymentMethod != EPaymentMethod.Cash && card is null)
            throw new ArgumentException("Payment Method with Card needs a user card defined", nameof(paymentMethod));

        return new Order
        {
            UserId = requestUserId, OrderStatus = requestOrderStatus,
            PaymentMethod = paymentMethod,
            CardFinalNumber = paymentMethod is EPaymentMethod.CreditCard or EPaymentMethod.DebitCard
                ? card!.CardFinalNumbers
                : null,
            Street = address.Street,
            HouseNumber = address.HouseNumber, PostalCode = address.PostalCode, City = address.City,
            Country = address.Country, Complement = address.Complement, Reference = address.Reference,
            Neighboorhood = address.Neighboorhood
        };
    }
}