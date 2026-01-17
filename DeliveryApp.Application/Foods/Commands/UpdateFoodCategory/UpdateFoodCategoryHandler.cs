using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Foods.Commands.UpdateFoodCategory;

public record UpdateFoodCategoryCommand(Guid Id, string Name, bool Active) : IRequest<Guid>;

public class UpdateFoodCategoryHandler(IFoodCategoryRepository foodCategoryRepository)
    : IRequestHandler<UpdateFoodCategoryCommand, Guid>
{
    public async Task<Guid> Handle(UpdateFoodCategoryCommand request, CancellationToken cancellationToken)
    {
        var foodCategory = await foodCategoryRepository.FindById(request.Id, cancellationToken) ??
                           throw new NotFoundException("Food Category not found");
        
        foodCategory.Update(request.Name, request.Active);

        await foodCategoryRepository.Update(foodCategory, cancellationToken);
        await foodCategoryRepository.SaveChanges(cancellationToken);

        return foodCategory.Id;
    }
}