namespace DeliveryApp.Domain.Entities.User;

public class Address : Entity
{
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Neighboorhood { get; set; } = null!;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}