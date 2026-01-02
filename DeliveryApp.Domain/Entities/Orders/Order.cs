using DeliveryApp.Domain.Entities.Users;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Domain.Entities.Orders;

public class Order : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public EOrderStatus EOrderStatus { get; set; }
}