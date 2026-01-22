using FluentValidation;

namespace DeliveryApp.Application.Users.Commands.CreateCard;

public class CreateCardRequestValidation : AbstractValidator<CreateCardCommand>
{
    public CreateCardRequestValidation()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required.");

        RuleFor(x => x.CardFinalNumbers)
            .Matches(@"^\d{4}$")
            .WithMessage("Invalid card number.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid card type. Must be Credit or Debit.");

        RuleFor(x => x.Brand)
            .IsInEnum().WithMessage("Invalid card brand.");
    }
}