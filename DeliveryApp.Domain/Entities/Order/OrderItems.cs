using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Domain.Entities.Order;

public class OrderItems : Entity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public Guid FoodId { get; set; }
    public Food.Food Food { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;

    public static IEnumerable<OrderItems> Create(IEnumerable<Food.Food> foodItems, Guid orderId)
    {
        return foodItems.GroupBy(f => f.Id).Select(foodItem => new OrderItems
            { OrderId = orderId, FoodId = foodItem.Key, Quantity = foodItem.Count() }).ToList();
    }
}