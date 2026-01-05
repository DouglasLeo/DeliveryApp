namespace DeliveryApp.Domain.Entities.Bag;

public class Bag : Entity
{
    public IEnumerable<Guid> FoodsIds { get; set; } = null!;
    public Guid UserId { get; set; }
    public User.User User { get; set; } = null!;
}