using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Domain.Entities.Address;

public class Address : Entity
{
    public Guid UserId { get; set; }
    public User.User User { get; set; }
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string Neighboorhood { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? Complement { get; set; }
    public string? Reference { get; set; }

    public void Update(string street, string houseNumber, string postalCode, string city,  string neighboorhood, string country, string? complement,string? reference)
    {
        Street = street;
        HouseNumber = houseNumber;
        PostalCode = postalCode;
        City = city;
        Neighboorhood = neighboorhood;
        Country = country;
        Complement = complement;
        Reference = reference;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
    
    public static Address Create(Guid userId, string street, string houseNumber, string postalCode, string city,
        string neighboorhood, string country, string? Complement,string? reference) =>
        new Address
        {
            UserId = userId,
            Street = street,
            HouseNumber = houseNumber,
            PostalCode = postalCode,
            City = city,
            Neighboorhood = neighboorhood,
            Country = country,
            Complement = Complement,
            Reference = reference
        };
}