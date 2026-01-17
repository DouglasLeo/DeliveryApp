using System.Linq.Expressions;
using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Domain.Entities.Food;

namespace DeliveryApp.Application.Common.Mappings;

public static class FoodDtoMappings
{
    public static Expression<Func<Food, FoodDto>> ToDtoExpression() =>
        u => new FoodDto(u.Id, u.Name, u.Description, u.Price,
            u.FoodImage != null ? u.FoodImage.ImageUrl : null,
            new FoodCategoryDto(u.FoodCategory.Id, u.FoodCategory.Name), u.Tags.Select(t => new TagDto(t.Id, t.Name)));

    public static FoodDto ToDto(this Food food) => new(food.Id, food.Name, food.Description, food.Price,
        food.FoodImage?.ImageUrl ?? "", FoodCategoryDtoMapperly.FromEntity(food.FoodCategory), food.Tags.Select(TagDtoMapperly.FromEntity));

    public static IQueryable<FoodDto> ProjectToModel(this IQueryable<Food> query) =>
        query.Select(ToDtoExpression());
}