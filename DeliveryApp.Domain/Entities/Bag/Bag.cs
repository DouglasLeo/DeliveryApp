using DeliveryApp.Domain.Entities.Bag.ValueObjects;
using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Domain.Entities.Bag;

public class Bag : Entity
{
    public List<BagItem> BagItems { get; set; } = null!;
    public Guid UserId { get; set; }
    public User.User User { get; set; } = null!;

    private Bag()
    {
    }

    public void Update(Dictionary<Guid, int> bagItems)
    {
        BagItems.Clear();

        BagItems = CreateBagItems(bagItems);
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public static Bag CreateOrUpdate(Bag? existingBag, Guid userId, Dictionary<Guid, int> bagItems)
    {
        if(existingBag is null)
            return Create(userId, bagItems);
        
        existingBag.Update(bagItems);
        return existingBag;
    }

    public static Bag Create(Guid userId, Dictionary<Guid, int> bagItems)
    {
        return new Bag
        {
            UserId = userId,
            BagItems = CreateBagItems(bagItems)
        };
    }

    private static List<BagItem> CreateBagItems(Dictionary<Guid, int> bagItems)
        => bagItems.Select(b => BagItem.Create(b.Key, b.Value)).ToList();
}