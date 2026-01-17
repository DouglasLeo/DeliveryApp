using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Domain.Entities.Food;

public class FoodCategory : Entity
{
    public string Name { get; set; } = null!;
    public bool Active { get; set; } = true;

    public static FoodCategory Create(string name) => new() { Name = name };

    public void Update(string requestName, bool requestActive)
    {
        Name = requestName;
        Active = requestActive;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}