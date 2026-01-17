namespace DeliveryApp.Application.Foods.Queries.DTOs;

public record FoodDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    string? ImageUrl,
    FoodCategoryDto FoodCategoryName,
    IEnumerable<TagDto> Tags);