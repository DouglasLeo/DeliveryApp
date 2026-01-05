namespace DeliveryApp.Domain.Entities.Food;

public class Food : Entity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public Guid FoodCategoryId { get; set; }
    public FoodCategory FoodCategory { get; set; } = null!;
    public bool Active { get; set; }
    public string? Description { get; set; }
    public Guid? TagId { get; set; }
    public Tag? Tag { get; set; }
}