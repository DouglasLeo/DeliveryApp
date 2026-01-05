using DeliveryApp.Application.Bags.Commands.UpdateBag;
using FluentValidation;

namespace DeliveryApp.Application.Bags.Commands.CreateBag;

public class UpdateBagRequestValidation : AbstractValidator<UpdateBagCommand>
{
    public UpdateBagRequestValidation()
    {
        RuleFor(x => x.FoodsIds).NotEmpty().WithMessage("Foods cannot be empty");
    }
}