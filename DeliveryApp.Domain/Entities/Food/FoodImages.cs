namespace DeliveryApp.Domain.Entities.Food;

public class FoodImages : Entity
{
    public Guid FoodId { get; set; }
    public Food Food { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}