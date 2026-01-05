using FluentValidation;

namespace DeliveryApp.Application.Users.Commands.CreateUser;

public class CreateUserRequestValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserRequestValidation()
    {
        RuleFor(x => x.Name).MinimumLength(3)
            .WithMessage("Name minimal length is 3.")
            .MaximumLength(250).WithMessage("Name maximum length is 250.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email needs to be a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("The password must be at least 8 characters.")
            .MaximumLength(100).WithMessage("The password must be a maximum 100 characters.")
            .Matches("[A-Z]").WithMessage("The password must be at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("The password must be at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("The password must be at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("The password must be at least one special character.");
    }
}