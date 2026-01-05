using FluentValidation;

namespace DeliveryApp.Application.Foods.Commands.CreateTag;

public class CreateTagRequestValidation : AbstractValidator<CreateTagCommand>
{
    public CreateTagRequestValidation()
    {
        RuleFor(request => request.Name).MinimumLength(3).WithMessage("Name minimal length is 3.").MaximumLength(50)
            .WithMessage("Name maximum length is 50");
    }
}