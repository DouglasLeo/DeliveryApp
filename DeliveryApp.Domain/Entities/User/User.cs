namespace DeliveryApp.Domain.Entities.User;

public class User : Entity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool Active { get; set; } = true;
    public Guid? CreditCardId { get; set; }
    public CreditCard? CreditCard { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }
}