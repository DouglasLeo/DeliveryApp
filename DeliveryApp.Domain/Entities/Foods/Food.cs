namespace DeliveryApp.Domain.Entities.Foods;

public class Food : Entity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<string> ImagesUrl { get; set; }
    public Guid FoodCategoryId { get; set; }
    public FoodCategory FoodCategory { get; set; }
    public bool Active { get; set; }
    public string? Description { get; set; }
    public Guid? TagId { get; set; }
    public Tag? Tag { get; set; }
}