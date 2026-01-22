using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.Users.Queries.Dtos;

public record CardDto(
    Guid Id,
    ECardType CardType,
    ECardBrand CardBrand,
    string CardFinalNumbers);