using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Domain.Entities.Food;

public class Food : Entity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public Guid FoodCategoryId { get; set; }
    public FoodCategory FoodCategory { get; set; } = null!;
    public bool Active { get; set; } = true;
    public string? Description { get; set; }
    public IEnumerable<Tag> Tags { get; set; } = null!;
    public FoodImage? FoodImage { get; set; }

    public void Update(string name, string? description, decimal price,  Guid foodCategoryId, bool active, IEnumerable<Tag> tags)
    {
        Name = name;
        Description = description;
        Price = price;
        FoodCategoryId = foodCategoryId;
        Active = active;
        Tags = tags;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
    
    public void UpdateImage(FoodImage foodImage) => FoodImage = foodImage;
    
    public static Food Create(string name, decimal price, Guid foodCategoryId, string? description,
        IEnumerable<Tag> tags) =>
        new Food
        {
            Name = name,
            Price = price,
            FoodCategoryId = foodCategoryId,
            Description = description,
            Tags = tags
        };
}