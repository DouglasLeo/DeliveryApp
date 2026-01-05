using FluentValidation;

namespace DeliveryApp.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderRequestValidation : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderRequestValidation()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(x => x.OrderStatus).IsInEnum().WithMessage("Order status is invalid");
    }
}