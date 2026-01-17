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
    IEnumerable<Guid> TagIds) : IRequest<Guid>;

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

        var tags = request.TagIds.Any()
            ? await tagRepository.FindTagsByIds(request.TagIds, cancellationToken)
            : [];

        food.Update(request.Name, request.Description, request.Price, request.FoodCategoryId, request.Active, tags);

        await foodRepository.Update(food, cancellationToken);
        await foodRepository.SaveChanges(cancellationToken);

        return food.Id;
    }
}