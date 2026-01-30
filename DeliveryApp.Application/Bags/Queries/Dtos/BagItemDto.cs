using DeliveryApp.Application.Foods.Queries.DTOs;

namespace DeliveryApp.Application.Bags.Queries.Dtos;

public record BagItemDto(FoodDto FoodDto, int Quantity);