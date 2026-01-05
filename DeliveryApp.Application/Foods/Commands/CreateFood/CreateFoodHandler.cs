using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Foods.Commands.CreateFood;

public record CreateFoodCommand(
    string Name,
    string? Description,
    decimal Price,
    Guid FoodCategoryId,
    bool Active,
    Guid? TagId) : IRequest<Guid>;

public class CreateFoodHandler(
    IFoodRepository foodRepository,
    IFoodCategoryRepository foodCategoryRepository,
    ITagRepository tagRepository) : IRequestHandler<CreateFoodCommand, Guid>
{
    public async Task<Guid> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
    {
        var category = await foodCategoryRepository.FindById(request.FoodCategoryId, cancellationToken) ??
                       throw new NotFoundException("Category not found");

        var food = new Food
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            FoodCategoryId = request.FoodCategoryId,
            FoodCategory = category,
            Active = request.Active,
        };

        if (request.TagId.HasValue)
        {
            var tag = await tagRepository.FindById(request.TagId.Value, cancellationToken) ??
                      throw new NotFoundException("Tag not found");

            food.Tag = tag;
            food.TagId = request.TagId;
        }

        await foodRepository.Add(food, cancellationToken);
        await foodRepository.SaveChanges(cancellationToken);

        return food.Id;
    }
}