using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Domain.Entities.Bag;

public class Bag : Entity
{
    public IEnumerable<Guid> FoodsIds { get; set; } = null!;
    public Guid UserId { get; set; }
    public User.User User { get; set; } = null!;

    public void Update(IEnumerable<Guid> foodsIds)
    {
        FoodsIds = foodsIds;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public static Bag Create(Guid userId, IEnumerable<Guid> foodsIds) =>
        new()
        {
            UserId = userId,
            FoodsIds = foodsIds
        };
}