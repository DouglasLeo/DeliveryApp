using FluentValidation;

namespace DeliveryApp.Application.Foods.Queries.GetFoods;

public class GetAllFoodsQueryRequestValidation : AbstractValidator<GetAllFoodsQuery>
{
    public GetAllFoodsQueryRequestValidation()
    {
        RuleFor(x => x.Take).GreaterThan(0);
    }
}