using System.Linq.Expressions;
using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Domain.Entities.Food;

namespace DeliveryApp.Application.Common.Mappings;

public static class FoodDtoMappings
{
    public static Expression<Func<Food, FoodDto>> ToDtoExpression() =>
        f => new FoodDto(f.Id, f.Name, f.Description, f.Price,
            f.FoodImage != null ? f.FoodImage.ImageUrl : null, f.Active,
            new FoodCategoryDto(f.FoodCategory.Id, f.FoodCategory.Name), f.Tags.Select(t => new TagDto(t.Id, t.Name)));

    public static FoodDto ToDto(this Food food) => new(food.Id, food.Name, food.Description, food.Price,
        food.FoodImage?.ImageUrl ?? "", food.Active,FoodCategoryDtoMapperly.FromEntity(food.FoodCategory), food.Tags.Select(TagDtoMapperly.FromEntity));

    public static IQueryable<FoodDto> ProjectToModel(this IQueryable<Food> query) =>
        query.Select(ToDtoExpression());
}