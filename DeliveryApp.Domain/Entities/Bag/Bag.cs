using DeliveryApp.Domain.Entities.Foods;
using DeliveryApp.Domain.Entities.Users;

namespace DeliveryApp.Domain.Entities.Bag;

public class Bag : Entity
{
    public IEnumerable<Food> Foods { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public decimal Total => Foods.Sum(f => f.Price);
}