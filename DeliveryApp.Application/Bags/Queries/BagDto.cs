using DeliveryApp.Application.Foods.Queries.DTOs;

namespace DeliveryApp.Application.Bags.Queries;

public record BagDto(Guid Id, IEnumerable<FoodDto> Foods, decimal TotalPrice);