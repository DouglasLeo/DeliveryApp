using FluentValidation;

namespace DeliveryApp.Application.Foods.Commands.UpdateFoodCategory;

public class UpdateFoodCategoryRequestValidation : AbstractValidator<UpdateFoodCategoryCommand>
{
    public UpdateFoodCategoryRequestValidation()
    {
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name minimal length is 3.").MaximumLength(150)
            .WithMessage("Name maximum length is 150.");
    }
}