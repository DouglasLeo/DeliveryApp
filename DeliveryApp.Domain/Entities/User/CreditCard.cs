namespace DeliveryApp.Domain.Entities.User;

public class CreditCard : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public int SecurityCode { get; set; }
    public string ExpirationDate { get; set; } = null!;
    public string NameOnCard { get; set; } = null!;
}