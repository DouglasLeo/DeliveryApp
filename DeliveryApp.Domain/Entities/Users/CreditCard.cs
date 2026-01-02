namespace DeliveryApp.Domain.Entities.Users;

public class CreditCard : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string CardNumber { get; set; }
    public int SecurityCode { get; set; }
    public string ExpirationDate { get; set; }
    public string NameOnCard { get; set; }
}