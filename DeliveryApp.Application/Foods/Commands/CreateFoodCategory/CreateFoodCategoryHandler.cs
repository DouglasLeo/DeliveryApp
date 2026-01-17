using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Foods.Commands.CreateFoodCategory;

public record CreateFoodCategoryCommand(string Name) : IRequest<Guid>;

public class CreateFoodCategoryHandler(IFoodCategoryRepository foodCategoryRepository)
    : IRequestHandler<CreateFoodCategoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateFoodCategoryCommand request, CancellationToken cancellationToken)
    {
        var foodCategory = await foodCategoryRepository.FindByName(request.Name, cancellationToken);

        if (foodCategory is not null)
            throw new AlreadyExistsException($"Food Category with name {request.Name} already exists.");

        var category = FoodCategory.Create(request.Name);

        await foodCategoryRepository.Add(category, cancellationToken);
        await foodCategoryRepository.SaveChanges(cancellationToken);

        return category.Id;
    }
}