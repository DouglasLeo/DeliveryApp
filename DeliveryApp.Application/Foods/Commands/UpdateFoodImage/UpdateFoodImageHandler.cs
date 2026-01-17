using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Application.Foods.Commands.UpdateFoodImage;

public record UpdateFoodImageCommand(Guid Id, IFormFile Image) : IRequest;

public class UpdateFoodImageHandler(
    IFoodRepository foodRepository,
    IFoodImageRepository foodImageRepository,
    IFileStorageService fileStorageService) : IRequestHandler<UpdateFoodImageCommand>
{
    public async Task Handle(UpdateFoodImageCommand request, CancellationToken cancellationToken)
    {
        var food = await foodRepository.FindById(request.Id, cancellationToken) ??
                   throw new NotFoundException("Food not found");
        var imageUrl = await fileStorageService.SaveImageAsync(request.Image, cancellationToken);

        var foodImage = FoodImage.Create(imageUrl, food);
        food.UpdateImage(foodImage);
        
        await foodImageRepository.Add(foodImage, cancellationToken);
        await foodRepository.Update(food, cancellationToken);
        
        await foodRepository.SaveChanges(cancellationToken);
    }
}