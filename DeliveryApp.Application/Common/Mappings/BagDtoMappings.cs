using DeliveryApp.Application.Bags.Queries;
using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Domain.Entities.Bag;

namespace DeliveryApp.Application.Common.Mappings;

public static class BagExtensions
{
    public static BagDto ToDto(this Bag bag, IEnumerable<FoodDto> foods)
    {
        var foodsList = foods.ToList();
        
        return new BagDto(bag.Id, foodsList, foodsList.Select(f => f.Price).Sum());
    }
}