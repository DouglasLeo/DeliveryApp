using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Application.Foods.Commands.CreateFood;

public record CreateFoodCommand(
    string Name,
    string? Description,
    decimal Price,
    Guid FoodCategoryId,
    IEnumerable<Guid> TagIds,
    IFormFile? Image) : IRequest<Guid>;

public class CreateFoodHandler(
    IFoodRepository foodRepository,
    IFoodCategoryRepository foodCategoryRepository,
    ITagRepository tagRepository,
    IFoodImageRepository foodImageRepository,
    IFileStorageService fileStorageService) : IRequestHandler<CreateFoodCommand, Guid>
{
    public async Task<Guid> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
    {
        var category = await foodCategoryRepository.FindById(request.FoodCategoryId, cancellationToken) ??
                       throw new NotFoundException("Category not found");

        var tags = request.TagIds.Any()
            ? await tagRepository.FindTagsByIds(request.TagIds, cancellationToken)
            : [];

        var food = Food.Create(request.Name, request.Price, request.FoodCategoryId, request.Description, tags);

        await foodRepository.Add(food, cancellationToken);
        
        if (request.Image is { Length: > 0 })
        {
            var imageUrl = await fileStorageService.SaveImageAsync(request.Image, cancellationToken);
            var foodImage = FoodImage.Create(imageUrl, food);
            await foodImageRepository.Add(foodImage, cancellationToken);
        }

        await foodRepository.SaveChanges(cancellationToken);

        return food.Id;
    }
}