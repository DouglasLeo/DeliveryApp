namespace DeliveryApp.Domain.Entities.User;

public class UserRole : Entity
{
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public Role Role { get; set; } = null!;
    public Guid RoleId { get; set; }
}