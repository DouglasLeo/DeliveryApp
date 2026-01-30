using DeliveryApp.Domain.Exceptions;

namespace DeliveryApp.Domain.Entities.Bag.ValueObjects;

public class BagItem
{
    public Guid FoodId { get; private set; }
    public int Quantity { get; private set; }
    
    public static BagItem Create(Guid foodId, int quantity)
    {
        if (quantity <= 0)
            throw new QuantityException("Quantity must be greater than zero");
        
        return new BagItem
        {
            FoodId = foodId,
            Quantity = quantity
        };
    }
}