using FluentValidation;

namespace DeliveryApp.Application.Foods.Queries.GetTags;

public class GetAllTagsQueryValidation : AbstractValidator<GetAllTagsQuery>
{
    public GetAllTagsQueryValidation()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}