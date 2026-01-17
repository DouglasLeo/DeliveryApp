using DeliveryApp.Domain.Entities.Shared;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Domain.Entities.User;

public class Card : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string CardFinalNumbers { get; set; } = null!;
    public string ExpirationMonth { get; set; } = null!;
    public string ExpirationYear { get; set; } = null!;
    public string HolderName { get; set; } = null!;
    public ECardBrand CardBrand { get; set; }
    public ECardType CardType { get; set; }

    public static Card Create(Guid requestUserId, string requestToken, string requestHolderName,
        string requestExpirationMonth, string requestExpirationYear, ECardType requestCardType,
        ECardBrand requestCardBrand, string requestCardFinalNumbers) =>
        new()
        {
            UserId = requestUserId, Token = requestToken, CardFinalNumbers = requestCardFinalNumbers,
            ExpirationMonth = requestExpirationMonth, ExpirationYear = requestExpirationYear,
            HolderName = requestHolderName, CardBrand = requestCardBrand, CardType = requestCardType
        };
}