using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Domain.Entities.Food;

public class FoodImage : Entity
{
    public Guid FoodId { get; set; }
    public Food Food { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;

    public static FoodImage Create(string imageUrl, Food food) =>
        new FoodImage { ImageUrl = imageUrl, FoodId = food.Id, Food = food };
}