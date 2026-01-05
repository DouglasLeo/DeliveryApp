using DeliveryApp.Domain.Entities.Food;
using Riok.Mapperly.Abstractions;

namespace DeliveryApp.Application.Foods.Queries.DTOs;

public record FoodCategoryDto(Guid Id, string Name);

[Mapper(RequiredEnumMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class FoodCategoryDtoMapper
{
    public static partial IQueryable<FoodCategoryDto> ProjectToModel(this IQueryable<FoodCategory> model);
}