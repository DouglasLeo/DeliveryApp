using FluentValidation;

namespace DeliveryApp.Application.Users.Commands.CreateCard;

public class CreateCardRequestValidation : AbstractValidator<CreateCardCommand>
{
    public CreateCardRequestValidation()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required.");

        RuleFor(x => x.HolderName)
            .MinimumLength(3).WithMessage("holder name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Holder Name cannot exceed 100 characters.");

        RuleFor(x => x.CardFinalNumbers).MaximumLength(4).WithMessage("Invalid card number.");

        RuleFor(x => x.ExpirationMonth)
            .NotEmpty().Matches("^(0[1-9]|1[0-2])$")
            .WithMessage("Expiration Month must be between 01 and 12.");

        RuleFor(x => x.ExpirationYear)
            .NotEmpty().Matches("^[0-9]{4}$")
            .WithMessage("Expiration Year must be a 4-digit year.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid card type. Must be Credit or Debit.");

        RuleFor(x => x.Brand)
            .IsInEnum().WithMessage("Invalid card brand.");
    }
}