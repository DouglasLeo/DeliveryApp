namespace DeliveryApp.Domain.Entities.Users;

public class UserRole : Entity
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Role Role { get; set; }
    public Guid RoleId { get; set; }
}