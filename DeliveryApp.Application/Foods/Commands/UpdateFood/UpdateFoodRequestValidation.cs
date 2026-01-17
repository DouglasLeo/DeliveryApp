using FluentValidation;

namespace DeliveryApp.Application.Foods.Commands.UpdateFood;

public class UpdateFoodRequestValidation : AbstractValidator<UpdateFoodCommand>
{
    public UpdateFoodRequestValidation()
    {
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name minimal length is 3.").MaximumLength(250)
            .WithMessage("Name maximum length is 250.");
        RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Description maximum length is 1000.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}