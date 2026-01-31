using FluentValidation;

namespace DeliveryApp.Application.Bags.Commands.CreateBag;

public class CreateBagRequestValidation : AbstractValidator<CreateBagCommand>
{
    public CreateBagRequestValidation()
    {
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Items cannot be empty");
        
        RuleForEach(x => x.Items)
            .Must(item => item.Value > 0)
            .WithMessage("Quantity must be greater than zero");
    }
}