using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Domain.Entities.User;

public class User : Entity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool Active { get; set; } = true;
    public Guid? CardId { get; set; }
    public Card? Card { get; set; }
    public List<Card> Cards { get; set; } = [];
    public Guid? AddressId { get; set; }
    public Address.Address? MainAddress { get; set; }
    public List<Address.Address> Addresses { get; set; } = [];
    public List<Role> Roles { get; set; } = [];
    public List<Order.Order> Orders { get; set; } = [];

    public void UpdateCard(Guid cardId) => CardId = cardId;

    public void RemoveCard() => CardId = null;

    public static User Create(string name, string email, string passwordHash)
    {
        return new User
        {
            Name = name,
            Email = email,
            Password = passwordHash
        };
    }

    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void InactivateUser()
    {
        Active = false;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateMainAddress(Address.Address address)
    {
        AddressId = address.Id;
        MainAddress = address;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdatePassword(string hashPassword)
    {
        Password = hashPassword;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}