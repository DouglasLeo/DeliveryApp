using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Domain.Entities.Food;
using Riok.Mapperly.Abstractions;

namespace DeliveryApp.Application.Common.Mappings;

[Mapper(RequiredEnumMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class FoodCategoryDtoMapperly
{
    public static partial FoodCategoryDto FromEntity(FoodCategory model);
    public static partial IQueryable<FoodCategoryDto> ProjectToModel(this IQueryable<FoodCategory> model);
}