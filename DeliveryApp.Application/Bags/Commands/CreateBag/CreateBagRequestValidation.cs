using FluentValidation;

namespace DeliveryApp.Application.Bags.Commands.CreateBag;

public class CreateBagRequestValidation : AbstractValidator<CreateBagCommand>
{
    public CreateBagRequestValidation()
    {
        RuleFor(x => x.FoodsIds).NotEmpty().WithMessage("Foods cannot be empty");
    }
}