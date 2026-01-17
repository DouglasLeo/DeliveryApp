using DeliveryApp.Application.Users.Commands.CreateUser;
using FluentValidation;

namespace DeliveryApp.Application.Users.Commands.UpdateUser;

public class UpdateUserRequestValidation: AbstractValidator<UpdateUserCommand>
{
    public UpdateUserRequestValidation()
    {
        RuleFor(x => x.Name).MinimumLength(3)
            .WithMessage("Name minimal length is 3.")
            .MaximumLength(250).WithMessage("Name maximum length is 250.");
        
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email needs to be a valid email address.");
    }
}