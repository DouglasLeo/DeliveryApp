using DeliveryApp.Application.Bags.Queries;
using DeliveryApp.Application.Bags.Queries.Dtos;
using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Domain.Entities.Bag;
using DeliveryApp.Domain.Exceptions;

namespace DeliveryApp.Application.Common.Mappings;

public static class BagExtensions
{
    public static BagDto ToDto(this Bag bag, IEnumerable<FoodDto> foods)
    {
        var foodsById = foods.ToDictionary(f => f.Id);

        var items = bag.BagItems.Select(bi =>
        {
            if (!foodsById.TryGetValue(bi.FoodId, out var food))
                throw new NotFoundException($"Food {bi.FoodId} not found");

            return new BagItemDto(food, bi.Quantity);
        }).ToList();

        var total = items.Sum(i => i.FoodDto.Price * i.Quantity);

        return new BagDto(bag.Id, items, total);
    }
}