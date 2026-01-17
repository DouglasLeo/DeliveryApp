using FluentValidation;

namespace DeliveryApp.Application.Foods.Queries.GetFoods;

public class GetFoodByNameQueryRequestValidation : AbstractValidator<GetFoodByNameQuery>
{
    public GetFoodByNameQueryRequestValidation()
    {
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name minimal length is 3.").MaximumLength(250)
            .WithMessage("Name maximum length is 250.");
    }
}