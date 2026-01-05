using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Foods.Commands.UpdateFoodCategory;

public record UpdateFoodCategoryCommand(string Name, bool Active) : IRequest<Guid>;

public class UpdateFoodCategoryHandler(IFoodCategoryRepository foodCategoryRepository)
    : IRequestHandler<UpdateFoodCategoryCommand, Guid>
{
    public async Task<Guid> Handle(UpdateFoodCategoryCommand request, CancellationToken cancellationToken)
    {
        var foodCategory = await foodCategoryRepository.FindByName(request.Name, cancellationToken) ??
                           throw new NotFoundException("Food Category not found");

        foodCategory.Name = request.Name;
        foodCategory.Active = request.Active;

        await foodCategoryRepository.Update(foodCategory, cancellationToken);
        await foodCategoryRepository.SaveChanges(cancellationToken);

        return foodCategory.Id;
    }
}