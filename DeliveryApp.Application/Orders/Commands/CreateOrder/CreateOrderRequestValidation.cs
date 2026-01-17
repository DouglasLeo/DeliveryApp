using FluentValidation;

namespace DeliveryApp.Application.Orders.Commands.CreateOrder;

public class CreateOrderRequestValidation : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderRequestValidation()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(x => x.OrderStatus).IsInEnum().WithMessage("OrderStatus is required");
        RuleFor(x => x.ItemsIds).NotEmpty().WithMessage("ItemsIds is required");
        RuleFor(x => x.PaymentMethod).IsInEnum().WithMessage("PaymentMethod must be an Enum");
    }
}