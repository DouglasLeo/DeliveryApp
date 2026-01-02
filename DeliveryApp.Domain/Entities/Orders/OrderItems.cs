using DeliveryApp.Domain.Entities.Foods;

namespace DeliveryApp.Domain.Entities.Orders;

public class OrderItems : Entity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public Guid FoodId { get; set; }
    public Food Food { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;
}