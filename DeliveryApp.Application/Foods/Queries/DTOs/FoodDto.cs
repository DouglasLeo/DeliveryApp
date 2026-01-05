namespace DeliveryApp.Application.Foods.Queries.DTOs;

public record FoodDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string FoodCategoryName,
    IEnumerable<string> Tags,
    IEnumerable<string> ImagesUrl);