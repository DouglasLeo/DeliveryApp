namespace DeliveryApp.Domain.Entities.Users;

public class Address : Entity
{
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Neighboorhood { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}