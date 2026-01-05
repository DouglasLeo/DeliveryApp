using FluentValidation;

namespace DeliveryApp.Application.Foods.Commands.CreateFoodCategory;

public class CreateFoodCategoryRequestValidation : AbstractValidator<CreateFoodCategoryCommand>
{
    public CreateFoodCategoryRequestValidation()
    {
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name minimal length is 3.").MaximumLength(150)
            .WithMessage("Name maximum length is 150.");
    }
}