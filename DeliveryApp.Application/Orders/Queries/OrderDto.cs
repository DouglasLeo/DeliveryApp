using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Domain.Enums;

namespace DeliveryApp.Application.Orders.Queries;

public record OrderDto(
    Guid Id,
    EOrderStatus EOrderStatus,
    DateTime Created,
    IEnumerable<FoodDto> FoodDtos,
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Neighboorhood,
    string Country,
    string? Reference,
    string? Complement);