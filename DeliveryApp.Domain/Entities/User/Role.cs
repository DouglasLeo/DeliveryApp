using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Domain.Entities.User;

public class Role : Entity
{
    public string Name { get; set; } = null!;
    public List<User> Users { get; set; } = [];
}