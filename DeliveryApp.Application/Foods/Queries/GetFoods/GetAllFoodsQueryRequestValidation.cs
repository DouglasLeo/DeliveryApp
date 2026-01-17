using FluentValidation;

namespace DeliveryApp.Application.Foods.Queries.GetFoods;

public class GetAllFoodsQueryRequestValidation : AbstractValidator<GetAllFoodsQuery>
{
    public GetAllFoodsQueryRequestValidation()
    {
        RuleFor(x => x.Take).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Take).InclusiveBetween(1, 100).WithMessage("Take must be between 1 and 100");
    }
}