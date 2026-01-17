using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Domain.Entities.Food;

public class Tag : Entity
{
    public string Name { get; set; } = null!;
    public List<Food> Foods { get; set; } = [];

    public static Tag Create(string requestName) => new() { Name = requestName };
}