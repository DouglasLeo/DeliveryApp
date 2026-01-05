using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Domain.Entities.Order;

public class Order : Entity
{
    public Guid UserId { get; set; }
    public User.User User { get; set; } = null!;
    public EOrderStatus EOrderStatus { get; set; }

    public static Order Create(Guid requestUserId, EOrderStatus requestOrderStatus)
        => new Order { UserId = requestUserId, EOrderStatus = requestOrderStatus };
}