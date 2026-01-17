using FluentValidation;

namespace DeliveryApp.Application.Adresses.Commands.CreateAddress;

public class CreateAddressRequestValidation : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressRequestValidation()
    {
        RuleFor(x => x.HouseNumber)
            .NotEmpty()
            .WithMessage("House Number is required")
            .MaximumLength(10)
            .WithMessage("House Number must not exceed 10 characters");
        
        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street is required")
            .MaximumLength(150)
            .WithMessage("Street must not exceed 150 characters");
        
        RuleFor(x => x.City).
            MinimumLength(3)
            .WithMessage("City must not exceed 3 characters")
            .MaximumLength(150)
            .WithMessage("City must not exceed 150 characters");
        
        RuleFor(x => x.Neighboorhood).
            MinimumLength(3)
            .WithMessage("Neighboorhood must not exceed 3 characters")
            .MaximumLength(150)
            .WithMessage("Neighboorhood must not exceed 150 characters");
        
        RuleFor(x => x.Country).
            MinimumLength(3)
            .WithMessage("Country must not exceed 3 characters")
            .MaximumLength(100)
            .WithMessage("Country must not exceed 100 characters");
        
        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .WithMessage("PostalCode is required.")
            .Matches(@"^\d{8}$")
            .WithMessage("Postcode must contains 8 digits.");
        
        RuleFor(x => x.Complement)
            .MaximumLength(300)
            .WithMessage("Reference must not exceed 300 characters");
        
        RuleFor(x => x.Reference)
            .MaximumLength(250)
            .WithMessage("Reference must not exceed 250 characters");
    }
}