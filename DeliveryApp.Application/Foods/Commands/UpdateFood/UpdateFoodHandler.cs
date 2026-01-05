using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Foods.Commands.UpdateFood;

public record UpdateFoodCommand(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    Guid FoodCategoryId,
    bool Active,
    Guid? TagId) : IRequest<Guid>;

public class UpdateFoodHandler(
    IFoodRepository foodRepository,
    IFoodCategoryRepository foodCategoryRepository,
    ITagRepository tagRepository) : IRequestHandler<UpdateFoodCommand, Guid>
{
    public async Task<Guid> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
    {
        var category = await foodCategoryRepository.FindById(request.FoodCategoryId, cancellationToken) ??
                       throw new NotFoundException("Category not found");

        var food = await foodRepository.FindById(request.Id, cancellationToken) ??
                   throw new NotFoundException("Food not found");

        food.Name = request.Name;
        food.Description = request.Description;
        food.Price = request.Price;
        food.FoodCategoryId = request.FoodCategoryId;
        food.FoodCategory = category;
        food.Active = request.Active;

        if (request.TagId.HasValue)
        {
            var tag = await tagRepository.FindById(request.TagId.Value, cancellationToken) ??
                      throw new NotFoundException("Tag not found");

            food.Tag = tag;
            food.TagId = request.TagId;
        }

        await foodRepository.Update(food, cancellationToken);
        await foodRepository.SaveChanges(cancellationToken);

        return food.Id;
    }
}