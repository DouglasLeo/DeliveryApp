using DeliveryApp.Domain.Entities.Orders;

namespace DeliveryApp.Domain.Entities.Users;

public class User : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid? CreditCardId { get; set; }
    public CreditCard? CreditCard { get; set; }
    public Guid AddressId { get; set; }
    public Address Address { get; set; }
    
}