namespace DeliveryApp.Domain.Entities.Food;

public class FoodCategory : Entity
{
    public string Name { get; set; } = null!;
    public bool Active { get; set; }
}