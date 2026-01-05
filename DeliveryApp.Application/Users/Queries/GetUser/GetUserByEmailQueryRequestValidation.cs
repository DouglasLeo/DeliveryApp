using FluentValidation;

namespace DeliveryApp.Application.Users.Queries.GetUser;

public class GetUserByEmailQueryRequestValidation : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryRequestValidation()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email needs to be a valid email address.");
    }
}